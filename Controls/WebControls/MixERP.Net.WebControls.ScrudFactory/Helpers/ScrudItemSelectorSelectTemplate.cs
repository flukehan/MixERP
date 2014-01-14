/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory.Helpers
{
    public class ScrudItemSelectorSelectTemplate : ITemplate, IDisposable
    {
        public HtmlAnchor selectAnchor;
        private bool disposed;
        public void InstantiateIn(Control container)
        {
            selectAnchor = new HtmlAnchor();
            selectAnchor.HRef = "#";
            selectAnchor.Attributes.Add("class", MixERP.Net.Common.Helpers.ConfigurationHelper.GetScrudParameter("ItemSelectorSelectAnchorCssClass"));
            selectAnchor.DataBinding += this.BindData;
            selectAnchor.InnerText = Resources.ScrudResource.Select;
            container.Controls.Add(selectAnchor);
        }

        public void BindData(object sender, EventArgs e)
        {
            using (GridViewRow container = (GridViewRow)selectAnchor.NamingContainer)
            {
                DataRowView rowView = container.DataItem as DataRowView;
                selectAnchor.Attributes.Add("onclick", "updateValue(" + rowView[0].ToString() + ");");
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
                    if (selectAnchor != null)
                    {
                        selectAnchor.DataBinding -= this.BindData;
                    }

                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(selectAnchor);
                }

                disposed = true;
            }
        }
    }
}
