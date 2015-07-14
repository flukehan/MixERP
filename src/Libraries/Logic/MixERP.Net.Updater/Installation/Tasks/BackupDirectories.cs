using System.ComponentModel;
using System.Globalization;
using System.IO;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Framework;
using MixERP.Net.i18n.Resources;

namespace MixERP.Net.Updater.Installation.Tasks
{
    internal class BackupDirectories : UpdateTask
    {
        public BackupDirectories(UpdaterConfig config)
            : base(config)
        {
        }
      
        public override string Description
        {
            get { return Titles.BackupDirectories; }
        }

        public override void Run()
        {
            string destination = this.Config.GetBackupDirectoryDestination();

            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            new DirectoryInfo(destination).Empty();

            foreach (string directory in this.Config.DirectoriesToBackup)
            {
                string path = PathHelper.Combine(Config.ApplicationPath, directory);
                string directoryName = new DirectoryInfo(directory).Name;

                destination = PathHelper.Combine(this.Config.GetBackupDirectoryDestination(), directoryName);


                string message = string.Format(CultureInfo.DefaultThreadCurrentCulture, Labels.BackingUp, path);


                this.OnProgress(new ProgressInfo(this.Description, message));
                DirectoryHelper.CopyDirectory(path, destination);
            }

            this.OnProgress(new ProgressInfo(this.Description, Labels.DirectoryBackupCompletedSuccessfully));
        }
    }
}