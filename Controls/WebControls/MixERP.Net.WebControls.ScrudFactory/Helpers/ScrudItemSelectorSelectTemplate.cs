/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Resources;

namespace MixERP.Net.WebControls.ScrudFactory.Helpers
{
    public class ScrudItemSelectorSelectTemplate : ITemplate, IDisposable
    {
        private HtmlAnchor selectAnchor;
        private bool disposed;
        public void InstantiateIn(Control container)
        {
            this.selectAnchor = new HtmlAnchor();

            this.selectAnchor.HRef = "#";
            this.selectAnchor.Attributes.Add("class", ConfigurationHelper.GetScrudParameter("ItemSelectorSelectAnchorCssClass"));
            this.selectAnchor.DataBinding += this.BindData;
            this.selectAnchor.InnerText = ScrudResource.Select;
            container.Controls.Add(this.selectAnchor);
        }

        public void BindData(object sender, EventArgs e)
        {
            using (var container = (GridViewRow)this.selectAnchor.NamingContainer)
            {
                var rowView = container.DataItem as DataRowView;
                if (rowView != null)
                {
                    this.selectAnchor.Attributes.Add("onclick", "updateValue(" + rowView[0] + ");");
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this.selectAnchor != null)
                    {
                        this.selectAnchor.DataBinding -= this.BindData;
                        this.selectAnchor.Dispose();
                        this.selectAnchor = null;
                    }
                }

                this.disposed = true;
            }
        }
    }
}
