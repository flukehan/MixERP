using System.ComponentModel;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Framework;
using MixERP.Net.i18n.Resources;

namespace MixERP.Net.Updater.Installation.Tasks
{
    internal class CopyUpdate : UpdateTask
    {
        public CopyUpdate(UpdaterConfig config) : base(config)
        {
        }

        public override string Description
        {
            get { return Titles.CopyNewApplication; }
        }

        public override void Run()
        {
            string source = this.Config.GetExtractDirectoryDestination();
            string destination = this.Config.ApplicationPath;

            this.OnProgress(new ProgressInfo(this.Description, Labels.DeletingApplicationFiles));

            DirectoryHelper.CopyDirectory(source, destination);

            this.OnProgress(new ProgressInfo(this.Description, Labels.DeletedApplicationFiles));
        }
    }
}