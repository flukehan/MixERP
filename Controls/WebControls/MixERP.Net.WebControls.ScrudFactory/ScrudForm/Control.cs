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
                throw new ApplicationException("The property 'TableSchema' cannot be left blank.");
            }

            if (string.IsNullOrWhiteSpace(this.Table))
            {
                throw new ApplicationException("The property 'Table' cannot be left blank.");
            }
            
            if (string.IsNullOrWhiteSpace(this.ViewSchema))
            {
                throw new ApplicationException("The property 'ViewSchema' cannot be left blank.");
            }

            if (string.IsNullOrWhiteSpace(this.View))
            {
                throw new ApplicationException("The property 'View' cannot be left blank.");
            }

            if (string.IsNullOrWhiteSpace(this.KeyColumn))
            {
                throw new ApplicationException("The property 'KeyColumn' cannot be left blank.");
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
                    
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(scrudContainer);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(formPanel);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(form);

                    if (formGridView != null)
                    {
                        formGridView.RowDataBound -= this.FormGridView_RowDataBound;                        
                    }
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(formGridView);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(addNewEntryLiteral);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(requiredFieldDetailsLabel);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(formContainer);

                    if (saveButton != null)
                    {
                        saveButton.Click -= this.SaveButton_Click;                        
                    }
                    
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(saveButton);

                    if (cancelButton != null)
                    {
                        cancelButton.Click -= this.CancelButton_Click;                    
                    }
                    
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(cancelButton);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(updatePanel);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(userIdHidden);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(officeCodeHidden);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(messageLabel);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(titleLabel);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(descriptionLabel);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(updateProgress);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(gridPanel);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(formGridView);
                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(pager);

                    if (topCommandPanel != null)
                    {
                        topCommandPanel.DeleteButtonClick -= this.DeleteButton_Click;
                        topCommandPanel.EditButtonClick -= this.EditButton_Click;                    
                    }

                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(topCommandPanel);

                    if (bottomCommandPanel != null)
                    {
                        bottomCommandPanel.DeleteButtonClick -= this.DeleteButton_Click;
                        bottomCommandPanel.EditButtonClick -= this.EditButton_Click;
                    }

                    MixERP.Net.Common.Helpers.DisposableHelper.DisposeObject(bottomCommandPanel);


                }

                disposed = true;
            }
        }


    }
}
