/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI.WebControls;
using MixERP.Net.Common;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm : CompositeControl
    {
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            this.Page.Validate();
            if (!this.Page.IsValid)
            {
                return;
            }

            string userIdSessionKey = MixERP.Net.Common.Helpers.ConfigurationHelper.GetScrudParameter("UserIdSessionKey");

            if (!(Conversion.TryCastInteger(MixERP.Net.Common.Helpers.SessionHelper.GetSessionValueByKey(userIdSessionKey)) > 0))
            {
                throw new ApplicationException("The user id session key is invalid or incorrectly configured.");
            }

            Collection<KeyValuePair<string, string>> list = this.GetFormCollection(true);
            string id = this.GetSelectedValue();

            int userId = Conversion.TryCastInteger(this.Page.Session[userIdSessionKey]);

            if(string.IsNullOrWhiteSpace(id))
            {
                if(DenyAdd)
                {
                    messageLabel.CssClass = "failure";
                    messageLabel.Text = Resources.ScrudResource.AccessDenied;
                }
                else
                {
                    if (MixERP.Net.WebControls.ScrudFactory.Data.FormHelper.InsertRecord(userId, this.TableSchema, this.Table, list, this.imageColumn))
                    {
                        //Clear the form container.
                        formContainer.Controls.Clear();

                        using (System.Data.DataTable table = new System.Data.DataTable())
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
                if(DenyEdit)
                {
                    messageLabel.CssClass = "failure";
                    messageLabel.Text = Resources.ScrudResource.AccessDenied;
                }
                else
                {
                    if (MixERP.Net.WebControls.ScrudFactory.Data.FormHelper.UpdateRecord(userId, this.TableSchema, this.Table, list, this.KeyColumn, id, this.imageColumn))
                    {
                        //Clear the form container.
                        formContainer.Controls.Clear();

                        //Load the form again.
                        using(System.Data.DataTable table = new System.Data.DataTable())
                        {
                            table.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;

                            this.LoadForm(this.formContainer, table);
                        }

                        //Refresh the grid.
                        this.BindGridView();

                        this.DisplaySuccess();
                    }
                    else
                    {
                        messageLabel.CssClass = "failure";
                        messageLabel.Text = Resources.ScrudResource.UnknownError;
                    }
                }
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(4000);
            //Clear the form.
            this.formContainer.Controls.Clear();

            //Clear grid selection.
            this.ClearSelectedValue();

            //Load the form again.
            using(System.Data.DataTable table = new System.Data.DataTable())
            {
                table.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                this.LoadForm(this.formContainer, table);
            }
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            string id = this.GetSelectedValue();
            if(string.IsNullOrWhiteSpace(id))
            {
                return;
            }

            using (System.Data.DataTable table = MixERP.Net.WebControls.ScrudFactory.Data.FormHelper.GetTable(this.TableSchema, this.Table, this.KeyColumn, id))
            {
                if(table.Rows.Count.Equals(1))
                {
                    //Clear the form container.
                    formContainer.Controls.Clear();

                    //Load the form again in the container with values 
                    //retrieved from database.
                    this.LoadForm(this.formContainer, table);
                    gridPanel.Attributes["style"] = "display:none;";
                    formPanel.Attributes["style"] = "display:block;";
                }
            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            string id = this.GetSelectedValue();
            if(string.IsNullOrWhiteSpace(id))
            {
                return;
            }

            if(DenyDelete)
            {
                messageLabel.CssClass = "failure";
                messageLabel.Text = Resources.ScrudResource.AccessDenied;
                return;
            }

            if (MixERP.Net.WebControls.ScrudFactory.Data.FormHelper.DeleteRecord(this.TableSchema, this.Table, this.KeyColumn, id))
            {
                //Refresh the grid.
                this.BindGridView();

                this.DisplaySuccess();
            }

        }
    }
}