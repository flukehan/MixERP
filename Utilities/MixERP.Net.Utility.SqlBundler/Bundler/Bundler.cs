using MixERP.Net.Utility.SqlBundler.Helpers;
using MixERP.Net.Utility.SqlBundler.Models;
using System.Collections.ObjectModel;

namespace MixERP.Net.Utility.SqlBundler
{
    public static class Bundler
    {
        public static void Bundle(string root, Collection<string> files)
        {
            foreach (string file in files)
            {
                BundlerModel model = Parser.Parse(root, file);
                if (model == null)
                {
                    return;
                }

                Collection<SQLBundle> bundles = Processor.Process(root, model);

                if (bundles == null)
                {
                    return;
                }

                IOHelper.WriteBundles(root, bundles);
            }
        }
    }
}