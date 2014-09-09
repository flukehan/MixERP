using MixERP.Net.BusinessLayer;
using MixERP.Net.BusinessLayer.Helpers;
using MixERP.Net.Common;
using System;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.Sales
{
    public partial class Index : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.LoadMenu();
            base.OnControlLoad(sender, e);
        }

        public void LoadMenu()
        {
            string menu = MenuHelper.GetPageMenu(this.Page);

            using (PlaceHolder menuPlaceHolder = PageUtility.FindControlIterative(this.Page, "MenuPlaceholder") as PlaceHolder)
            {
                if (menuPlaceHolder != null)
                {
                    using (Literal menuLiteral = new Literal())
                    {
                        menuLiteral.Text = menu;
                        menuPlaceHolder.Controls.Add(menuLiteral);
                    }
                }
            }
        }

        protected void SaveButton_Click(object sedner, EventArgs e)
        {
            this.Page.Response.Redirect("~/");
        }

        public int Divide(int a, int b)
        {
            return a / b;
        }
    }
}