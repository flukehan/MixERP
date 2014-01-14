/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using MixERP.Net.WebControls.ScrudFactory.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudItemSelector : CompositeControl, IDisposable
    {
        public Panel container;
        public DropDownList filterDropDownList;
        public TextBox filterTextBox;
        public GridView searchGridView;
        public Button goButton;

        private bool disposed;

        protected override void CreateChildControls()
        {
            container = new Panel();
            this.AddJavaScript();
            this.LoadItemSelector(container);
            this.Initialize();
            this.Controls.Add(container);
        }


        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }

        protected override void Render(HtmlTextWriter w)
        {
            container.RenderControl(w);
        }

        public override void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
            base.Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(container);

                    if (filterDropDownList != null)
                    {
                        filterDropDownList.DataBound -= this.FilterDropDownList_DataBound;
                    }

                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(filterDropDownList);

                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(filterTextBox);

                    if (searchGridView != null)
                    {
                        searchGridView.RowDataBound -= this.SearchGridView_RowDataBound;
                    }

                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(searchGridView);

                    if (goButton != null)
                    {
                        goButton.Click -= this.GoButton_Click;
                    }

                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(goButton);
                }

                disposed = true;
            }
        }

    }
}
