using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MixERP.Net.Utility.Installer.Helpers
{
    public static class FileHelper
    {
        public static string GetBaseDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetAssembly(typeof(FileHelper)).Location);
        }

        public static string CombineWithBaseDirectory(string path)
        {
            return Path.Combine(GetBaseDirectory(), path);
        }

        internal static string ReadSqlResource(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return string.Empty;
            }

            string resourceName = "MixERP.Net.Utility.Installer.Configs.SQL." + name;

            Assembly assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string result = reader.ReadToEnd();

                        return result;
                    }
                }
            }

            return string.Empty;
        }

        public static void CopyFilesRecursively(string source, string target, bool createTarget)
        {
            if (!Directory.Exists(source)) return;

            if (!Directory.Exists(target))
            {
                if (createTarget)
                {
                    Directory.CreateDirectory(target);
                }
                else
                {
                    return;
                }
            }

            CopyFilesRecursively(new DirectoryInfo(source), new DirectoryInfo(target));
        }

        public static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name));
        }

        public static void SetPermission(string directory, FileSystemRights permission, string userId)
        {
            if (!Directory.Exists(directory))
            {
                return;
            }

            DirectoryInfo info = new DirectoryInfo(directory);
            DirectorySecurity security = info.GetAccessControl();

            FileSystemAccessRule rule = new FileSystemAccessRule(userId, permission, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                PropagationFlags.None, AccessControlType.Allow);

            security.AddAccessRule(rule);
            info.SetAccessControl(security);

            // Maybe, we are trying too much and too hard. 
            // Windows 7 does not like this and would stop responding at times.
            // IDK why, but for some weird reasons, sleeping the thread 
            // solves "freezing" issues.
            System.Threading.Thread.Sleep(1000);
        }


    }
}
