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
    public static class Processor
    {
        public static Collection<SQLBundle> Process(string root, BundlerModel model)
        {
            if (model == null)
            {
                return null;
            }

            StringBuilder script = new StringBuilder();
            Collection<SQLBundle> bundles = new Collection<SQLBundle>();

            foreach (string fileName in model.Files)
            {
                if (!string.IsNullOrWhiteSpace(script.ToString()))
                {
                    script.Append(Environment.NewLine);
                }

                Console.WriteLine(@"Adding {0} to bundle", fileName);
                script.Append("-->-->-- ");
                script.Append(Path.GetFullPath(fileName).Replace(root, "").Replace("\\", "/"));
                script.Append(" --<--<--");

                script.Append(Environment.NewLine);

                script.Append(File.ReadAllText(fileName, Encoding.UTF8));
                script.Append(Environment.NewLine);
            }

            if (string.IsNullOrWhiteSpace(script.ToString()))
            {
                return null;
            }

            SQLBundle defaultBundle = new SQLBundle();
            defaultBundle.FileName = GetBundleFileName(model.OutputDirectory, model.OriginalFileName,
                model.DefaultLanguage);
            defaultBundle.Language = model.DefaultLanguage;
            defaultBundle.Script = script.ToString();

            bundles.Add(defaultBundle);

            foreach (var dictionary in model.Dictionaries)
            {
                Console.WriteLine(@"SQL localization dictionary: {0}", dictionary.Value);

                SQLBundle bundle = new SQLBundle();
                string filePath = Path.Combine(root, dictionary.Value);

                bundle.FileName = GetBundleFileName(model.OutputDirectory, model.OriginalFileName, dictionary.Key);
                bundle.Script = script.ToString();

                List<string> lines =
                    File.ReadAllText(filePath, Encoding.UTF8)
                        .Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                        .ToList();
                foreach (string line in lines)
                {
                    string find = line.Split('=')[0].Trim();
                    string replace = line.Split('=')[1].Trim();

                    bundle.Script = bundle.Script.Replace(find, replace);
                }

                bundles.Add(bundle);
            }

            return bundles;
        }

        private static string GetBundleFileName(string outputDirectory, string fileName, string language)
        {
            return outputDirectory + "/" + fileName + "." + language + ".sql";
        }
    }
}