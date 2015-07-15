using System.Globalization;
using System.IO;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Framework;
using MixERP.Net.i18n.Resources;

namespace MixERP.Net.Updater.Installation.Tasks
{
    internal class RestoreDirectories : UpdateTask
    {
        public RestoreDirectories(UpdaterConfig config)
            : base(config)
        {
        }

        public override string Description
        {
            get { return Titles.RestoringDirectories; }
        }

        public override void Run()
        {
            string sourceDirectory = this.Config.GetBackupDirectoryDestination();

            foreach (string directory in this.Config.DirectoriesToBackup)
            {
                string extractedPath = this.Config.GetExtractDirectoryDestination();
                string destination = PathHelper.Combine(extractedPath, directory);

                string directoryName = new DirectoryInfo(directory).Name;
                string source = PathHelper.Combine(sourceDirectory, directoryName);


                string message = string.Format(CultureInfo.DefaultThreadCurrentCulture, Labels.RestoringDirectory, destination);

                this.OnProgress(new ProgressInfo(this.Description, message));
                DirectoryHelper.CopyDirectory(source, destination);
            }

            this.OnProgress(new ProgressInfo(this.Description, Labels.DirectoryRestoreSuccessful));
        }
    }
}