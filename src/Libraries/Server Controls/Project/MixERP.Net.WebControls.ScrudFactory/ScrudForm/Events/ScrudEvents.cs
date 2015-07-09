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

using MixERP.Net.Framework;
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.ScrudFactory.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;

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
                this.LoadForm(this.formContainer, table);
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
                    throw new MixERPException(Titles.AccessIsDenied);
                }

                if (FormHelper.DeleteRecord(this.Catalog, this.TableSchema, this.Table, this.KeyColumn, id))
                {
                    //Refresh the grid.
                    this.BindGridView();

                    this.DisplaySuccess();
                }
            }
            catch (MixERPException ex)
            {
                this.DisplayError(ex);
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            string id = this.GetSelectedValue();
            if (string.IsNullOrWhiteSpace(id))
            {
                return;
            }

            using (DataTable table = FormHelper.GetTable(this.Catalog, this.TableSchema, this.Table, this.KeyColumn, id, this.KeyColumn))
            {
                if (table.Rows.Count.Equals(1))
                {
                    //Clear the form container.
                    this.formContainer.Controls.Clear();

                    //Load the form again in the container with values
                    //retrieved from database.
                    this.LoadForm(this.formContainer, table, true);
                    this.gridPanel.Attributes["style"] = "display:none;";
                    this.formPanel.Attributes["style"] = "display:block;";
                }
            }
        }

        // ReSharper disable once UnusedParameter.Local
        private void Save(bool closeForm)
        {
            int userId = this.UserId;

            if (userId <= 0)
            {
                throw new InvalidOperationException(Errors.InvalidUserId);
            }

            Collection<KeyValuePair<string, object>> list = this.GetFormCollection(true);
            string id = this.GetSelectedValue();
            this.lastValueHiddenTextBox.Text = id;


            if (string.IsNullOrWhiteSpace(id))
            {
                try
                {
                    if (this.DenyAdd)
                    {
                        throw new MixERPException(Titles.AccessIsDenied);
                    }

                    long lastValue = FormHelper.InsertRecord(this.Catalog, userId, this.TableSchema, this.Table, this.KeyColumn, list, this.imageColumn);

                    if (lastValue > 0)
                    {
                        this.lastValueHiddenTextBox.Text = lastValue.ToString(CultureInfo.InvariantCulture);
                        //Clear the form container.
                        this.formContainer.Controls.Clear();

                        using (DataTable table = new DataTable())
                        {
                            //Load the form again.
                            this.LoadForm(this.formContainer, table);
                        }

                        //Refresh the grid.
                        this.BindGridView();
                        this.DisplaySuccess();
                    }
                }
                catch (MixERPException ex)
                {
                    this.DisplayError(ex);
                }
            }
            else
            {
                try
                {
                    if (this.DenyEdit)
                    {
                        throw new MixERPException(Titles.AccessIsDenied);
                    }

                    string[] exclusion = {""};

                    if (!string.IsNullOrWhiteSpace(this.ExcludeEdit))
                    {
                        exclusion = this.ExcludeEdit.Split(',').Select(x => x.Trim().ToUpperInvariant()).ToArray();
                    }


                    if (FormHelper.UpdateRecord(this.Catalog, userId, this.TableSchema, this.Table, list, this.KeyColumn, id, this.imageColumn, exclusion))
                    {
                        //Clear the form container.
                        this.formContainer.Controls.Clear();

                        //Load the form again.
                        using (DataTable table = new DataTable())
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