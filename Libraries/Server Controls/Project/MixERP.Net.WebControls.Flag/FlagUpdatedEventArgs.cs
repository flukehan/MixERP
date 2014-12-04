using System;

namespace MixERP.Net.WebControls.Flag
{
    public class FlagUpdatedEventArgs : EventArgs
    {
        public FlagUpdatedEventArgs(int flagId)
        {
            this.FlagId = flagId;
        }

        public int FlagId { get; set; }
    }
}