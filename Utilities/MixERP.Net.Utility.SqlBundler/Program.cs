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

using System;
using System.Collections.ObjectModel;
using System.IO;

namespace MixERP.Net.Utility.SqlBundler
{
    internal class Program
    {
        private static string bundlePath = string.Empty;
        private static string root = string.Empty;

        public static void SetBundleDirectory(string path)
        {
            if (Directory.Exists(Path.Combine(root, path)))
            {
                bundlePath = Path.Combine(root, path);
            }
        }

        public static void SetRootDirectory(string dir)
        {
            if (Directory.Exists(dir))
            {
                root = dir;
            }
        }

        private static void Main(string[] args)
        {
            if (args[0] == null)
            {
                return;
            }

            SetRootDirectory(args[0]);

            if (string.IsNullOrWhiteSpace(root))
            {
                return;
            }

            if (args[1] == null)
            {
                return;
            }

            SetBundleDirectory(args[1]);

            if (string.IsNullOrWhiteSpace(bundlePath))
            {
                return;
            }

            Console.WriteLine(@"---------MixERP.Net.Utility.SqlBundler---------");

            Collection<string> files = new Collection<string>();

            foreach (var file in Directory.GetFiles(bundlePath))
            {
                if (file != null)
                {
                    if (Path.GetExtension(file).Equals(".sqlbundle"))
                    {
                        files.Add(file);
                    }
                }
            }

            if (files.Count > 0)
            {
                Bundler.Bundle(root, files);
            }

            Console.WriteLine(@"---------MixERP.Net.Utility.SqlBundler---------");
        }
    }
}