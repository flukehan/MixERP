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

namespace MixERP.Net.WebControls.ScrudFactory
{
    [ToolboxData("<{0}:ScrudForm runat=server></{0}:ScrudForm>")]
    public partial class ScrudForm : CompositeControl, IDisposable
    {
        private bool disposed;
        Panel scrudContainer;
        private string imageColumn = string.Empty;

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.TableSchema))
            {
                throw new ApplicationException("The property 'TableSchema' cannot be left empty.");
            }

            if (string.IsNullOrWhiteSpace(this.Table))
            {
                throw new ApplicationException("The property 'Table' cannot be left empty.");
            }
            
            if (string.IsNullOrWhiteSpace(this.ViewSchema))
            {
                throw new ApplicationException("The property 'ViewSchema' cannot be left empty.");
            }

            if (string.IsNullOrWhiteSpace(this.View))
            {
                throw new ApplicationException("The property 'View' cannot be left empty.");
            }

            if (string.IsNullOrWhiteSpace(this.KeyColumn))
            {
                throw new ApplicationException("The property 'KeyColumn' cannot be left empty.");
            }


        }
        
        protected override void CreateChildControls()
        {
            this.Validate();

            scrudContainer = new Panel();

            this.LoadScrudContainer(scrudContainer);

            this.LoadTitle();
            this.LoadDescription();
            
            this.LoadGrid();
            
            this.InitializeScrudControl();
            
            this.Controls.Add(scrudContainer);
        }


        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }

        protected override void Render(HtmlTextWriter w)
        {
            scrudContainer.RenderControl(w);
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

                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(this.scrudContainer);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(this.formPanel);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(this.form);

                    if (formGridView != null)
                    {
                        formGridView.RowDataBound -= this.FormGridView_RowDataBound;                        
                    }
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(this.formGridView);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(this.addNewEntryLiteral);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(this.requiredFieldDetailsLabel);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(this.formContainer);

                    if (this.saveButton != null)
                    {
                        this.saveButton.Click -= this.SaveButton_Click;                        
                    }

                    //if (this.SaveButtonClick != null)
                    //{
                    //    this.SaveButtonClick = null;
                    //}

                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(this.saveButton);

                    if (this.cancelButton != null)
                    {
                        cancelButton.Click -= this.CancelButton_Click;                    
                    }

                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(this.cancelButton);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(this.updatePanel);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(this.userIdHidden);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(this.officeCodeHidden);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(this.messageLabel);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(this.titleLabel);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(this.descriptionLabel);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(this.updateProgress);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(this.gridPanel);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(this.formGridView);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(this.pager);

                    if (this.topCommandPanel != null)
                    {
                        this.topCommandPanel.DeleteButtonClick -= this.DeleteButton_Click;
                        this.topCommandPanel.EditButtonClick -= this.EditButton_Click;                    
                    }

                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(this.topCommandPanel);

                    if (this.bottomCommandPanel != null)
                    {
                        this.bottomCommandPanel.DeleteButtonClick -= this.DeleteButton_Click;
                        this.bottomCommandPanel.EditButtonClick -= this.EditButton_Click;
                    }

                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(this.bottomCommandPanel);


                }

                disposed = true;
            }
        }


    }
}
