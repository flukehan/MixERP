/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudItemSelector : CompositeControl
    {
        public Panel container;
        public DropDownList filterDropDownList;
        public TextBox filterTextBox;
        public GridView searchGridView;
        public Button goButton;

        private bool disposed;

        protected override void CreateChildControls()
        {
            this.container = new Panel();
            this.AddJavaScript();
            this.LoadItemSelector(this.container);
            this.Initialize();
            this.Controls.Add(this.container);
        }


        protected override void RecreateChildControls()
        {
            this.EnsureChildControls();
        }

        protected override void Render(HtmlTextWriter w)
        {
            this.container.RenderControl(w);
        }

        public sealed override void Dispose()
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
                    if (this.container != null)
                    {
                        this.container.Dispose();
                        this.container = null;
                    }

                    if (this.filterDropDownList != null)
                    {
                        this.filterDropDownList.DataBound -= this.FilterDropDownList_DataBound;
                        this.filterDropDownList.Dispose();
                        this.filterDropDownList = null;
                    }

                    if (this.filterTextBox != null)
                    {
                        this.filterTextBox.Dispose();
                        this.filterTextBox = null;
                    }


                    if (this.searchGridView != null)
                    {
                        this.searchGridView.RowDataBound -= this.SearchGridView_RowDataBound;
                        this.searchGridView.Dispose();
                        this.searchGridView = null;
                    }

                    if (this.goButton != null)
                    {
                        this.goButton.Click -= this.GoButton_Click;
                        this.goButton.Dispose();
                        this.goButton = null;
                    }
                }

                this.disposed = true;
            }
        }

    }
}
