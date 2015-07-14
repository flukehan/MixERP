using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using MixERP.Net.Framework;
using MixERP.Net.Updater.Installation.Models;
using MixERP.Net.Common.Helpers;
using MixERP.Net.i18n.Resources;

namespace MixERP.Net.Updater.Installation.Tasks
{
    internal class MigrationBackup : UpdateTask
    {
        private readonly string[] files;
        private readonly string path;

        public MigrationBackup(UpdaterConfig config) : base(config)
        {
            this.path = Config.ApplicationPath;
            this.files = Config.Migrate.Split(',');
        }

        public override string Description
        {
            get { return Titles.MigratingFiles; }
        }

        public override void Run()
        {
            //Nothing to do here
        }

        public IEnumerable<MigrationFile> Backup()
        {
            List<MigrationFile> migrationFiles = new List<MigrationFile>();

            foreach (string file in files)
            {
                string filePath = PathHelper.Combine(path, file.Trim());
                string contents = File.ReadAllText(filePath);

                MigrationFile migrationFile = new MigrationFile
                {
                    FilePath = filePath,
                    Contents = contents
                };

                string message = string.Format(CultureInfo.DefaultThreadCurrentCulture, Labels.BackingUpForMigration, filePath);
                this.OnProgress(new ProgressInfo(this.Description, message));

                migrationFiles.Add(migrationFile);
            }

            return migrationFiles;
        }
    }
}