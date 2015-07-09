/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using MixERP.Net.Common.Helpers;
using MixERP.Net.i18n.Resources;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory.Helpers
{
    internal sealed class ScrudItemSelectorSelectTemplate : ITemplate, IDisposable
    {
        public string Catalog { get; set; }

        public ScrudItemSelectorSelectTemplate(string catalog)
        {
            this.Catalog = catalog;
        }

        private HtmlAnchor selectAnchor;

        public void InstantiateIn(Control container)
        {
            this.selectAnchor = new HtmlAnchor();

            this.selectAnchor.HRef = "#";
            this.selectAnchor.Attributes.Add("class",
                DbConfig.GetScrudParameter(this.Catalog, "ItemSelectorSelectAnchorCssClass"));
            this.selectAnchor.DataBinding += this.BindData;
            this.selectAnchor.InnerText = Titles.Select;
            container.Controls.Add(this.selectAnchor);
        }

        public void BindData(object sender, EventArgs e)
        {
            using (GridViewRow container = (GridViewRow) this.selectAnchor.NamingContainer)
            {
                DataRowView rowView = container.DataItem as DataRowView;
                if (rowView != null)
                {
                    this.selectAnchor.Attributes.Add("onclick", "sisUpdateValue('" + rowView[0] + "');");
                }
            }
        }

        #region IDisposable

        private bool disposed;

        public void Dispose()
        {
            if (!this.disposed)
            {
                this.Dispose(true);
            }
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (this.selectAnchor != null)
            {
                this.selectAnchor.DataBinding -= this.BindData;
                this.selectAnchor.Dispose();
                this.selectAnchor = null;
            }

            this.disposed = true;
        }

        #endregion
    }
}