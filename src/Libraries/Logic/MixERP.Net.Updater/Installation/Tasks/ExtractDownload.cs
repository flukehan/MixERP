using System.ComponentModel;
using System.IO;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Framework;
using MixERP.Net.i18n.Resources;

namespace MixERP.Net.Updater.Installation.Tasks
{
    internal class ExtractDownload : UpdateTask
    {
        public ExtractDownload(UpdaterConfig config) : base(config)
        {
        }

        public override string Description
        {
            get { return Titles.ExtractingDownload; }
        }

        public override void Run()
        {
            string archive = this.Config.GetDownloadDestination();
            string extractTo = this.Config.GetExtractDirectoryDestination();

            new DirectoryInfo(extractTo).Empty();

            this.OnProgress(new ProgressInfo(this.Description, Labels.ExtractingDownloadedFile));

            FileSystemHelper.Unzip(archive, extractTo);

            this.OnProgress(new ProgressInfo(this.Description, Labels.ExtractionCompleted));
        }
    }
}