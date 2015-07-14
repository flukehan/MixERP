namespace MixERP.Net.Updater.Installation.Models
{
    internal sealed class MigrationFile
    {
        public MigrationFile()
        {
        }

        public MigrationFile(string filePath, string contents)
        {
            this.FilePath = filePath;
            this.Contents = contents;
        }

        internal string FilePath { get; set; }
        internal string Contents { get; set; }
    }
}