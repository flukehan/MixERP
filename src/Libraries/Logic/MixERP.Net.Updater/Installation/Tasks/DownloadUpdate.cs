using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using MixERP.Net.Common;
using MixERP.Net.Framework;
using MixERP.Net.i18n.Resources;

namespace MixERP.Net.Updater.Installation.Tasks
{
    internal class DownloadUpdate : UpdateTask
    {
        public DownloadUpdate(UpdaterConfig config)
            : base(config)
        {
        }

        public override string Description
        {
            get { return Titles.DownloadingUpdate; }
        }

        public override void Run()
        {
            string temp = PageUtility.MapPath(this.Config.TempPath);
            string downloadFrom = this.Config.DownloadUrl;
            this.Download(temp, downloadFrom, this.Config.GetDownloadDestination());
        }

        private void Download(string temp, string downloadFrom, string destination)
        {
            if (!Directory.Exists(temp))
            {
                Directory.CreateDirectory(temp);
            }

            string message = string.Format(CultureInfo.DefaultThreadCurrentCulture, Labels.DownloadingUpdateFrom, downloadFrom);

            this.OnProgress(new ProgressInfo(this.Description, message));

            Task task = Task.Run(async () => { await this.DownloadTask(downloadFrom, destination); });
            task.Wait();
        }

        private Task DownloadTask(string downloadFrom, string destination)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadProgressChanged += this.OnDownloadProgress;
                client.DownloadFileCompleted += this.OnDownloadComplete;

                return client.DownloadFileTaskAsync(new Uri(downloadFrom), destination);
            }
        }

        private void OnDownloadComplete(object sender, AsyncCompletedEventArgs e)
        {
            string message = Labels.DownloadSuccessful;

            if (e.Cancelled || e.Error != null)
            {
                message = e.Error.Message;
            }

            this.OnProgress(new ProgressInfo(this.Description, message));
        }

        private void OnDownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            string message = string.Format(CultureInfo.DefaultThreadCurrentCulture, Labels.PercentCompleted, e.ProgressPercentage);

            this.OnProgress(new ProgressInfo(this.Description, message));
        }
    }
}