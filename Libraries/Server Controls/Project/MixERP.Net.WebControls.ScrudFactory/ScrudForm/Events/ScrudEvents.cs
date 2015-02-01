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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.UI;
using MixERP.Net.Common;
using MixERP.Net.Common.Base;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Data;
using MixERP.Net.WebControls.ScrudFactory.Resources;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        public event EventHandler SaveButtonClick;
        public event EventHandler UseButtonClick;

        private void CancelButton_Click(object sender, EventArgs e)
        {
            //Clear the form.
            this.formContainer.Controls.Clear();

            //Clear grid selection.
            this.ClearSelectedValue();

            //Load the form again.
            using (DataTable table = new DataTable())
            {
                table.Locale = Thread.CurrentThread.CurrentCulture;
                this.LoadForm(this.formContainer, table, this.ResourceAssembly);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            string id = this.GetSelectedValue();
            if (string.IsNullOrWhiteSpace(id))
            {
                return;
            }

            try
            {
                if (this.DenyDelete)
                {
                    throw new MixERPException(Titles.AccessDenied);
                }

                if (FormHelper.DeleteRecord(this.TableSchema, this.Table, this.KeyColumn, id))
                {
                    //Refresh the grid.
                    this.BindGridView();

                    this.DisplaySuccess();
                }
            }
            catch (MixERPException ex)
            {
                this.DisplayError(ex);
                //Swallow
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            string id = this.GetSelectedValue();
            if (string.IsNullOrWhiteSpace(id))
            {
                return;
            }

            using (DataTable table = FormHelper.GetTable(this.TableSchema, this.Table, this.KeyColumn, id, this.KeyColumn))
            {
                if (table.Rows.Count.Equals(1))
                {
                    //Clear the form container.
                    this.formContainer.Controls.Clear();

                    //Load the form again in the container with values
                    //retrieved from database.
                    this.LoadForm(this.formContainer, table, this.ResourceAssembly, true);
                    this.gridPanel.Attributes["style"] = "display:none;";
                    this.formPanel.Attributes["style"] = "display:block;";
                }
            }
        }

        // ReSharper disable once UnusedParameter.Local
        private void Save(bool closeForm)
        {
            string userIdSessionKey = ConfigurationHelper.GetScrudParameter("UserIdSessionKey");

            if (Conversion.TryCastInteger(CurrentSession.GetSessionValueByKey(userIdSessionKey)) <= 0)
            {
                throw new InvalidOperationException("The user id session key is invalid or incorrectly configured.");
            }

            Collection<KeyValuePair<string, object>> list = this.GetFormCollection(true);
            string id = this.GetSelectedValue();
            this.lastValueHiddenTextBox.Text = id;

            int userId = Conversion.TryCastInteger(this.Page.Session[userIdSessionKey]);

            if (string.IsNullOrWhiteSpace(id))
            {
                try
                {
                    if (this.DenyAdd)
                    {
                        throw new MixERPException(Titles.AccessDenied);
                    }

                    long lastValue = FormHelper.InsertRecord(userId, this.TableSchema, this.Table, list, this.imageColumn);

                    if (lastValue > 0)
                    {
                        this.lastValueHiddenTextBox.Text = lastValue.ToString(CultureInfo.InvariantCulture);
                        //Clear the form container.
                        this.formContainer.Controls.Clear();

                        using (DataTable table = new DataTable())
                        {
                            //Load the form again.
                            this.LoadForm(this.formContainer, table, this.ResourceAssembly);
                        }

                        //Refresh the grid.
                        this.BindGridView();
                        this.DisplaySuccess();
                    }
                }
                catch (MixERPException ex)
                {
                    this.DisplayError(ex);
                    //Swallow
                }

            }
            else
            {
                try
                {
                    if (this.DenyEdit)
                    {
                        throw new MixERPException(Titles.AccessDenied);
                    }

                    string[] exclusion = { "" };

                    if (!string.IsNullOrWhiteSpace(this.ExcludeEdit))
                    {
                        exclusion = this.ExcludeEdit.Split(',').Select(x => x.Trim().ToUpperInvariant()).ToArray();
                    }


                    if (FormHelper.UpdateRecord(userId, this.TableSchema, this.Table, list, this.KeyColumn, id, this.imageColumn, exclusion))
                    {
                        //Clear the form container.
                        this.formContainer.Controls.Clear();

                        //Load the form again.
                        using (DataTable table = new DataTable())
                        {
                            table.Locale = Thread.CurrentThread.CurrentCulture;

                            this.LoadForm(this.formContainer, table, this.ResourceAssembly);
                        }

                        //Refresh the grid.
                        this.BindGridView();

                        this.DisplaySuccess();
                    }
                    else
                    {
                        this.DisplayError(new MixERPException(Titles.UnknownError));
                    }
                }
                catch (MixERPException ex)
                {
                    this.DisplayError(ex);
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
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

            this.Save(false);
        }

        private void UseButton_Click(object sender, EventArgs e)
        {
            this.Page.Validate();
            if (!this.Page.IsValid)
            {
                return;
            }

            if (this.UseButtonClick != null)
            {
                this.UseButtonClick(sender, e);
                return;
            }

            this.Save(true);
        }
    }
}