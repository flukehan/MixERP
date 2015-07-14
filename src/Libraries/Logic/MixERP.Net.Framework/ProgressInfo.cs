using System;

namespace MixERP.Net.Framework
{
    public delegate void ProgressHandler(ProgressInfo progressInfo);

    public sealed class ProgressInfo
    {
        public ProgressInfo(string description, string message)
        {
            this.Timestamp = DateTime.UtcNow;
            this.Description = description;
            this.Message = message;
        }

        public string Description { get; set; }
        public DateTime Timestamp { get; private set; }
        public string Message { get; set; }
    }
}