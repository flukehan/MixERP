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

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        private bool disposed;

        public override void Dispose()
        {
            if (!this.disposed)
            {
                this.Dispose(true);
                base.Dispose();
            }
        }

        private void Dispose(bool disposing)
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

                    if (this.requiredFieldDetailsLiteral != null)
                    {
                        this.requiredFieldDetailsLiteral.Dispose();
                        this.requiredFieldDetailsLiteral = null;
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

                    //Important: Don't sent this event to null.
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