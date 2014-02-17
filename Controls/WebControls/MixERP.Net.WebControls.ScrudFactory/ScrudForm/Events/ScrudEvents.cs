/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Resources;
using FormHelper = MixERP.Net.WebControls.ScrudFactory.Data.FormHelper;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        public event EventHandler SaveButtonClick;

        [SuppressMessage("Microsoft.Globalization", "CA1306:SetLocaleForDataTypes")]
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            this.Page.Validate();
            if (!this.Page.IsValid)
            {
                return;
            }

            if (this.SaveButtonClick != null)
            {
                this.SaveButtonClick(sender, e);
                return;
            }
            
            var userIdSessionKey = ConfigurationHelper.GetScrudParameter("UserIdSessionKey");

            if (!(Conversion.TryCastInteger(SessionHelper.GetSessionValueByKey(userIdSessionKey)) > 0))
            {
                throw new InvalidOperationException("The user id session key is invalid or incorrectly configured.");
            }

            var list = this.GetFormCollection(true);
            var id = this.GetSelectedValue();

            var userId = Conversion.TryCastInteger(this.Page.Session[userIdSessionKey]);

            if(string.IsNullOrWhiteSpace(id))
            {
                if(this.DenyAdd)
                {
                    this.messageLabel.CssClass = "failure";
                    this.messageLabel.Text = ScrudResource.AccessDenied;
                }
                else
                {
                    if (FormHelper.InsertRecord(userId, this.TableSchema, this.Table, list, this.imageColumn))
                    {
                        //Clear the form container.
                        this.formContainer.Controls.Clear();

                        using (var table = new DataTable())
                        {
                            //Load the form again.
                            this.LoadForm(this.formContainer, table);
                        }

                        //Refresh the grid.
                        this.BindGridView();
                        this.DisplaySuccess();

                    }
                }
            }
            else
            {
                if(this.DenyEdit)
                {
                    this.messageLabel.CssClass = "failure";
                    this.messageLabel.Text = ScrudResource.AccessDenied;
                }
                else
                {
                    if (FormHelper.UpdateRecord(userId, this.TableSchema, this.Table, list, this.KeyColumn, id, this.imageColumn))
                    {
                        //Clear the form container.
                        this.formContainer.Controls.Clear();

                        //Load the form again.
                        using(var table = new DataTable())
                        {
                            table.Locale = Thread.CurrentThread.CurrentCulture;

                            this.LoadForm(this.formContainer, table);
                        }

                        //Refresh the grid.
                        this.BindGridView();

                        this.DisplaySuccess();
                    }
                    else
                    {
                        this.messageLabel.CssClass = "failure";
                        this.messageLabel.Text = ScrudResource.UnknownError;
                    }
                }
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            //Clear the form.
            this.formContainer.Controls.Clear();

            //Clear grid selection.
            this.ClearSelectedValue();

            //Load the form again.
            using(var table = new DataTable())
            {
                table.Locale = Thread.CurrentThread.CurrentCulture;
                this.LoadForm(this.formContainer, table);
            }
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            var id = this.GetSelectedValue();
            if(string.IsNullOrWhiteSpace(id))
            {
                return;
            }

            using (var table = FormHelper.GetTable(this.TableSchema, this.Table, this.KeyColumn, id))
            {
                if(table.Rows.Count.Equals(1))
                {
                    //Clear the form container.
                    this.formContainer.Controls.Clear();

                    //Load the form again in the container with values 
                    //retrieved from database.
                    this.LoadForm(this.formContainer, table);
                    this.gridPanel.Attributes["style"] = "display:none;";
                    this.formPanel.Attributes["style"] = "display:block;";
                }
            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            var id = this.GetSelectedValue();
            if(string.IsNullOrWhiteSpace(id))
            {
                return;
            }

            if(this.DenyDelete)
            {
                this.messageLabel.CssClass = "failure";
                this.messageLabel.Text = ScrudResource.AccessDenied;
                return;
            }

            if (FormHelper.DeleteRecord(this.TableSchema, this.Table, this.KeyColumn, id))
            {
                //Refresh the grid.
                this.BindGridView();

                this.DisplaySuccess();
            }

        }
    }
}