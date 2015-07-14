using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using MixERP.Net.Framework;
using MixERP.Net.i18n.Resources;
using MixERP.Net.Updater.Installation.Models;

namespace MixERP.Net.Updater.Installation.Tasks
{
    internal class MigrationRestore : UpdateTask
    {
        public MigrationRestore(UpdaterConfig config, IEnumerable<MigrationFile> migrationFiles) : base(config)
        {
            this.MigrationFiles = migrationFiles;
        }

        internal IEnumerable<MigrationFile> MigrationFiles { get; private set; }

        public override string Description
        {
            get { return Titles.RestoringMigrationFiles; }
        }

        public override void Run()
        {
            foreach (MigrationFile file in this.MigrationFiles)
            {
                string message = string.Format(CultureInfo.DefaultThreadCurrentCulture, Labels.RestoringFile, file.FilePath);
                this.OnProgress(new ProgressInfo(this.Description, message));

                File.Delete(file.FilePath);
                File.WriteAllText(file.FilePath, file.Contents, Encoding.UTF8);
            }
        }
    }
}