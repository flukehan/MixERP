/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using MixERP.Net.Utility.SqlBundler.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace MixERP.Net.Utility.SqlBundler.Helpers
{
    public static class Parser
    {
        public static BundlerModel Parse(string root, string path, bool includeOptionalFiles)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            BundlerModel model = new BundlerModel();
            //Collection<string> files = new Collection<string>();
            Collection<KeyValuePair<string, string>> dictionaries = new Collection<KeyValuePair<string, string>>();

            string content = File.ReadAllText(path, Encoding.UTF8);
            string scriptDirectory = string.Empty;

            string[] lines = content.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                var items = line.Split(':');
                var key = items[0].TrimStart('-').Trim();
                var value = items[1].Trim();

                string defaultLanguage = GetDefaultLanguage(key, value);

                if (!string.IsNullOrWhiteSpace(GetScriptDirectory(key, value)))
                {
                    scriptDirectory = GetScriptDirectory(key, value);
                    scriptDirectory = Path.Combine(root, scriptDirectory);

                    if (!Directory.Exists(scriptDirectory))
                    {
                        scriptDirectory = string.Empty;
                    }
                }

                KeyValuePair<string, string> dictionary = GetDictionary(key, value);
                string outputDirectory = GetOutputDirectory(key, value);

                if (!string.IsNullOrWhiteSpace(defaultLanguage))
                {
                    model.DefaultLanguage = defaultLanguage;
                }

                if (!string.IsNullOrWhiteSpace(dictionary.Key) && !string.IsNullOrWhiteSpace(dictionary.Value))
                {
                    dictionaries.Add(dictionary);
                }

                if (!string.IsNullOrWhiteSpace(outputDirectory))
                {
                    model.OutputDirectory = outputDirectory;
                }
            }

            model.OriginalFileName = Path.GetFileNameWithoutExtension(path).Replace(".sqlbundle", "");
            model.Files = GetScripts(scriptDirectory, includeOptionalFiles);
            model.Dictionaries = dictionaries;

            return model;
        }

        private static string GetDefaultLanguage(string key, string value)
        {
            if (key.Equals("default-language"))
            {
                return value;
            }

            return String.Empty;
        }

        private static KeyValuePair<string, string> GetDictionary(string key, string value)
        {
            if (key.Equals("dictionary"))
            {
                value = value.TrimStart('[').TrimEnd(']');

                return new KeyValuePair<string, string>(value.Split(',')[0].Trim(), value.Split(',')[1].Trim());
            }

            return new KeyValuePair<string, string>();
        }

        private static IEnumerable<string> GetFiles(string path, bool includeOptionalFiles)
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(path);

            while (queue.Count > 0)
            {
                path = queue.Dequeue();

                try
                {
                    foreach (string subDir in Directory.GetDirectories(path))
                    {
                        queue.Enqueue(subDir);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }

                string[] files = null;
                try
                {
                    if (includeOptionalFiles)
                    {
                        files = GetFiles(path, "*.sql|*.optional");
                    }
                    else
                    {
                        files = GetFiles(path, "*.sql");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }
                if (files != null)
                {
                    foreach (string t in files)
                    {
                        yield return t;
                    }
                }
            }
        }

        private static string[] GetFiles(string sourceFolder, string filters)
        {
            return filters.Split('|').SelectMany(filter => Directory.GetFiles(sourceFolder, filter)).ToArray();
        }

        private static string GetOutputDirectory(string key, string value)
        {
            if (key.Equals("output-directory"))
            {
                return value;
            }

            return String.Empty;
        }

        private static string GetScriptDirectory(string key, string value)
        {
            if (key.Equals("script-directory"))
            {
                return value;
            }

            return string.Empty;
        }

        private static Collection<string> GetScripts(string directory, bool includeOptionalFiles)
        {
            return new Collection<string>(GetFiles(directory, includeOptionalFiles).OrderBy(s => s).ToList());
        }
    }
}