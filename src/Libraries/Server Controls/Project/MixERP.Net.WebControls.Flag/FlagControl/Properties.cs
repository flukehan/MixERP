using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.Flag
{
    public partial class FlagControl
    {
        private DropDownList flagDropDownlist;
        public string Catalog { get; set; }
        public string AssociatedControlId { get; set; }
        public override string CssClass { get; set; }
        public override string ID { get; set; }
        public string OnClientClick { get; set; }
    }
}