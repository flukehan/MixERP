using MixERP.Net.Framework;

namespace MixERP.Net.Updater.Installation
{
    public abstract class UpdateTask : IUpdateTask
    {
        protected UpdateTask(UpdaterConfig config)
        {
            this.Config = config;
        }

        public UpdaterConfig Config { get; set; }
        public virtual string Description { get; set; }
        public event ProgressHandler Progress;
        public abstract void Run();

        public virtual void OnProgress(ProgressInfo progressInfo)
        {
            ProgressHandler handler = this.Progress;

            if (handler != null)
            {
                handler(progressInfo);
            }
        }
    }
}