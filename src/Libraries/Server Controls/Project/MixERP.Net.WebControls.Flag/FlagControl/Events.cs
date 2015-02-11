using MixERP.Net.Common;
using System;

namespace MixERP.Net.WebControls.Flag
{
    public partial class FlagControl
    {
        public delegate void FlagUpdated(object sender, FlagUpdatedEventArgs e);

        public event FlagUpdated Updated;

        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            if (this.Updated != null)
            {
                this.Updated(this, new FlagUpdatedEventArgs(this.GetFlagTypeId()));
            }
        }

        private int GetFlagTypeId()
        {
            int flagTypeId = Conversion.TryCastInteger(this.flagDropDownlist.SelectedValue);
            return flagTypeId;
        }
    }
}