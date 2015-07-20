using System.IO;
using System.Text;
using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Helpers;
using MixERP.Net.DbFactory;
using MixERP.Net.Framework;
using MixERP.Net.i18n.Resources;

namespace MixERP.Net.Updater.Installation.Tasks
{
    internal class RunPatch : UpdateTask
    {
        public RunPatch(UpdaterConfig config)
            : base(config)
        {
        }

        public override string Description
        {
            get { return Titles.RunningDatabasePatch; }
        }

        public override void Run()
        {
            string extractedPath = this.Config.GetExtractDirectoryDestination();
            string path = PathHelper.Combine(extractedPath, "db/patch.sql");

            if (File.Exists(path))
            {
                this.OnProgress(new ProgressInfo(this.Description, Labels.PatchingDatabase));

                string sql = File.ReadAllText(path, Encoding.UTF8);

                DbOperation.ExecuteNonQuery(AppUsers.GetCurrentUserDB(), sql);

                this.OnProgress(new ProgressInfo(this.Description, Labels.PatchedDatabase));
            }
        }
    }
}