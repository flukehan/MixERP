using System.IO;
using MixERP.Net.Common;
using MixERP.Net.Framework;
using MixERP.Net.Common.Helpers;
using MixERP.Net.i18n.Resources;

namespace MixERP.Net.Updater.Installation
{
    public class UpdaterConfig
    {
        public string ApplicationPath { get; set; }
        public string Migrate { get; set; }
        public string DownloadUrl { get; set; }
        public string TempPath { get; set; }
        public string[] DirectoriesToBackup { get; set; }

        public string GetDownloadDestination()
        {
            string temp = PageUtility.MapPath(this.TempPath);
            string downloadFrom = this.DownloadUrl;
            string fileName = Path.GetFileName(downloadFrom);

            if (fileName == null)
            {
                throw new MixERPException(Errors.CannotDetermineFileFromDownloadUrl);
            }

            string destination = PathHelper.Combine(temp, fileName);

            return destination;
        }

        public string GetBackupDirectoryDestination()
        {
            string temp = PageUtility.MapPath(this.TempPath);
            string destination = PathHelper.Combine(temp, "Backups");

            return destination;
        }

        public string GetExtractDirectoryDestination()
        {
            string archive = this.GetDownloadDestination();

            if (!File.Exists(archive))
            {
                throw new MixERPException(Errors.InvalidFileLocation);
            }

            string archiveDirectory = Path.GetDirectoryName(archive);

            if (archiveDirectory != null)
            {
                return PathHelper.Combine(archiveDirectory, Path.GetFileNameWithoutExtension(archive));
            }

            return string.Empty;
        }
    }
}