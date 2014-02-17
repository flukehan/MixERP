/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;
using MixERP.Net.WebControls.ScrudFactory.Resources;

namespace MixERP.Net.WebControls.ScrudFactory
{
    [ToolboxData("<{0}:ScrudForm runat=server></{0}:ScrudForm>")]
    public partial class ScrudForm
    {
        private bool disposed;
        Panel scrudContainer;
        private string imageColumn = string.Empty;

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.TableSchema))
            {
                throw new InvalidOperationException(ScrudResource.TableSchemaEmptyExceptionMessage);
            }

            if (string.IsNullOrWhiteSpace(this.Table))
            {
                throw new InvalidOperationException(ScrudResource.TableEmptyExceptionMessage);
            }

            if (string.IsNullOrWhiteSpace(this.ViewSchema))
            {
                throw new InvalidOperationException(ScrudResource.ViewSchemaEmptyExceptionMessage);
            }

            if (string.IsNullOrWhiteSpace(this.View))
            {
                throw new InvalidOperationException(ScrudResource.ViewEmptyExceptionMessage);
            }

            if (string.IsNullOrWhiteSpace(this.KeyColumn))
            {
                throw new InvalidOperationException(ScrudResource.KeyColumnEmptyExceptionMessage);
            }


        }

        protected override void CreateChildControls()
        {
            this.Validate();

            this.scrudContainer = new Panel();

            this.LoadScrudContainer(this.scrudContainer);

            this.LoadTitle();
            this.LoadDescription();

            this.LoadGrid();

            this.InitializeScrudControl();

            this.Controls.Add(this.scrudContainer);
        }


        protected override void RecreateChildControls()
        {
            this.EnsureChildControls();
        }

        protected override void Render(HtmlTextWriter w)
        {
            this.scrudContainer.RenderControl(w);
        }

        public sealed override void Dispose()
        {
            if (!this.disposed)
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
                base.Dispose();
            }
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this.scrudContainer != null)
                    {
                        this.scrudContainer.Dispose();
                        this.scrudContainer = null;
                    }

                    if (this.formPanel != null)
                    {
                        this.formPanel.Dispose();
                        this.formPanel = null;
                    }

                    if (this.form != null)
                    {
                        this.form.Dispose();
                        this.form = null;
                    }

                    if (this.formGridView != null)
                    {
                        this.formGridView.RowDataBound -= this.FormGridView_RowDataBound;
                        this.formGridView.Dispose();
                        this.formGridView = null;
                    }

                    if (this.addNewEntryLiteral != null)
                    {
                        this.addNewEntryLiteral.Dispose();
                        this.addNewEntryLiteral = null;
                    }

                    if (this.requiredFieldDetailsLabel != null)
                    {
                        this.requiredFieldDetailsLabel.Dispose();
                        this.requiredFieldDetailsLabel = null;
                    }

                    if (this.formContainer != null)
                    {
                        this.formContainer.Dispose();
                        this.formContainer = null;
                    }


                    if (this.saveButton != null)
                    {
                        this.saveButton.Click -= this.SaveButton_Click;
                    }

                    //Do not set the event to null.
                    //if (this.SaveButtonClick != null)
                    //{
                    //    this.SaveButtonClick = null;
                    //}

                    if (this.saveButton != null)
                    {
                        this.saveButton.Dispose();
                        this.saveButton = null;
                    }


                    if (this.cancelButton != null)
                    {
                        this.cancelButton.Click -= this.CancelButton_Click;
                    }

                    if (this.cancelButton != null)
                    {
                        this.cancelButton.Dispose();
                        this.cancelButton = null;
                    }

                    if (this.updatePanel != null)
                    {
                        this.updatePanel.Dispose();
                        this.updatePanel = null;
                    }

                    if (this.userIdHidden != null)
                    {
                        this.userIdHidden.Dispose();
                        this.userIdHidden = null;
                    }

                    if (this.officeCodeHidden != null)
                    {
                        this.officeCodeHidden.Dispose();
                        this.officeCodeHidden = null;
                    }

                    if (this.messageLabel != null)
                    {
                        this.messageLabel.Dispose();
                        this.messageLabel = null;
                    }

                    if (this.titleLabel != null)
                    {
                        this.titleLabel.Dispose();
                        this.titleLabel = null;
                    }

                    if (this.descriptionLabel != null)
                    {
                        this.descriptionLabel.Dispose();
                        this.descriptionLabel = null;
                    }

                    if (this.updateProgress != null)
                    {
                        this.updateProgress.Dispose();
                        this.updateProgress = null;
                    }

                    if (this.gridPanel != null)
                    {
                        this.gridPanel.Dispose();
                        this.gridPanel = null;
                    }

                    if (this.formGridView != null)
                    {
                        this.formGridView.Dispose();
                        this.formGridView = null;
                    }

                    if (this.pager != null)
                    {
                        this.pager.Dispose();
                        this.pager = null;
                    }


                    if (this.topCommandPanel != null)
                    {
                        this.topCommandPanel.DeleteButtonClick -= this.DeleteButton_Click;
                        this.topCommandPanel.EditButtonClick -= this.EditButton_Click;
                        this.topCommandPanel.Dispose();
                        this.topCommandPanel = null;
                    }


                    if (this.bottomCommandPanel != null)
                    {
                        this.bottomCommandPanel.DeleteButtonClick -= this.DeleteButton_Click;
                        this.bottomCommandPanel.EditButtonClick -= this.EditButton_Click;
                        this.bottomCommandPanel.Dispose();
                        this.bottomCommandPanel = null;
                    }

                }

                this.disposed = true;
            }
        }


    }
}
