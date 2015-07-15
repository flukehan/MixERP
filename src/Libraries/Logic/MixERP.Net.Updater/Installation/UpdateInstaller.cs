using System.Collections.Generic;
using System.Web;
using MixERP.Net.Framework;
using MixERP.Net.i18n.Resources;
using MixERP.Net.Updater.Installation.Models;
using MixERP.Net.Updater.Installation.Tasks;

namespace MixERP.Net.Updater.Installation
{
    public class UpdateInstaller
    {
        public delegate void UpdateProgressHandler(ProgressInfo progressInfo);

        private IEnumerable<MigrationFile> files;

        public UpdateInstaller(string downloadUrl, string[] toBackup)
        {
            this.DownloadUrl = downloadUrl;
            this.ToBackup = toBackup;
        }

        public string DownloadUrl { get; private set; }
        public string[] ToBackup { get; private set; }
        public event UpdateProgressHandler Progress;

        public void Update()
        {
            UpdaterConfig config = this.GetConfig();

            this.CreateMigrationFileBackup(config);

            var tasks = this.GetTasks(config);

            foreach (UpdateTask task in tasks)
            {
                task.Progress += this.OnProgress;
                task.Run();
            }

            this.RestoreMigrationFileBackup(config);
            this.OnProgress(new ProgressInfo(Titles.SuccessfullyUpdated, Labels.UpdateOperationCompletedSuccessfully));
        }

        private IEnumerable<UpdateTask> GetTasks(UpdaterConfig config)
        {
            List<UpdateTask> tasks = new List<UpdateTask>();

            tasks.Add(new BackupDirectories(config));
            tasks.Add(new DownloadUpdate(config));
            tasks.Add(new ExtractDownload(config));
            tasks.Add(new RestoreDirectories(config));
            tasks.Add(new RunPatch(config));
            tasks.Add(new RemoveApplication(config));
            tasks.Add(new CopyUpdate(config));

            return tasks;
        }

        private UpdaterConfig GetConfig()
        {
            UpdaterConfig config = new UpdaterConfig();

            config.DownloadUrl = this.DownloadUrl;

            if (Config.IsDevelopmentMode())
            {
                config.DownloadUrl = this.GetFakeDownloadUrl();
            }

            config.ApplicationPath = Config.ApplicationPath;
            config.Migrate = Config.Migrate;
            config.TempPath = Config.TempPath;
            config.DirectoriesToBackup = this.ToBackup;
            return config;
        }

        private string GetFakeDownloadUrl()
        {
            return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority +
                   HttpContext.Current.Request.ApplicationPath + "update.zip";
        }

        protected virtual void OnProgress(ProgressInfo progressInfo)
        {
            UpdateProgressHandler handler = this.Progress;

            if (handler != null)
            {
                handler(progressInfo);
            }
        }

        #region Migration

        private void CreateMigrationFileBackup(UpdaterConfig config)
        {
            MigrationBackup backup = new MigrationBackup(config);
            backup.Progress += this.OnProgress;
            this.files = backup.Backup();
        }

        private void RestoreMigrationFileBackup(UpdaterConfig config)
        {
            MigrationRestore restore = new MigrationRestore(config, this.files);
            restore.Progress += this.OnProgress;
            restore.Run();
        }

        #endregion
    }
}