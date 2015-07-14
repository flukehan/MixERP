using MixERP.Net.Framework;

namespace MixERP.Net.Updater.Installation
{

    public interface IUpdateTask
    {
        string Description { get; set; }
        event ProgressHandler Progress;
        void Run();
        void OnProgress(ProgressInfo progressInfo);
    }
}