using System;
using System.Collections.ObjectModel;
using System.IO;

namespace MixERP.Net.Utility.SqlBundler
{
    internal class Program
    {
        private static string root = string.Empty;
        private static string bundlePath = string.Empty;

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

            Console.WriteLine("---------MixERP.Net.Utility.SqlBundler---------");

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

            Console.WriteLine("---------MixERP.Net.Utility.SqlBundler---------");
        }

        public static void SetRootDirectory(string dir)
        {
            if (Directory.Exists(dir))
            {
                root = dir;
            }
        }

        public static void SetBundleDirectory(string path)
        {
            if (Directory.Exists(Path.Combine(root, path)))
            {
                bundlePath = Path.Combine(root, path);
            }
        }
    }
}