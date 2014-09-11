using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace MixERP.Net.Common.Base
{
    public class MixERPUserControlBase : UserControl
    {
        public string MasterPageId { get; set; }

        public string TargetContentPlaceHolder { get; set; }

        public string OverridePath { get; set; }

        public virtual void OnControlLoad(object sender, EventArgs e)
        {
        }
    }
}