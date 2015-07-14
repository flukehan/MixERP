using System.ComponentModel;
using System.IO;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Framework;
using MixERP.Net.i18n.Resources;

namespace MixERP.Net.Updater.Installation.Tasks
{
    internal class RemoveApplication : UpdateTask
    {
        public RemoveApplication(UpdaterConfig config) : base(config)
        {
        }

        public override string Description
        {
            get { return Titles.RemovingApplication; }
        }

        public override void Run()
        {
            string path = Config.ApplicationPath;

            if (!Directory.Exists(path))
            {
                throw new MixERPException(Errors.CannotDetermineAppDirectoryPath);
            }

            this.OnProgress(new ProgressInfo(this.Description, Labels.DeletedApplicationFiles));
            new DirectoryInfo(path).Empty();
            this.OnProgress(new ProgressInfo(this.Description, Labels.DeletedApplicationFiles));
        }
    }
}