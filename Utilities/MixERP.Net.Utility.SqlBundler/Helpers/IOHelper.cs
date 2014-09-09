using MixERP.Net.Utility.SqlBundler.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MixERP.Net.Utility.SqlBundler.Helpers
{
    public static class IOHelper
    {
        public static void WriteBundles(string root, IEnumerable<SQLBundle> bundles)
        {
            foreach (var bundle in bundles)
            {
                string filePath = Path.Combine(root, bundle.FileName);
                Console.WriteLine("Writing bundle {0}", filePath);
                File.WriteAllText(filePath, bundle.Script, Encoding.UTF8);
            }
        }
    }
}