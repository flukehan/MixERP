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

using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.DBFactory;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.FrontEnd.Data.Office
{
    public static class Offices
    {
        public static Collection<Common.Models.Office.Office> GetOffices()
        {
            const string sql = "SELECT * FROM office.get_offices();";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                return GetOffices(DbOperations.GetDataTable(command));
            }
        }

        private static Common.Models.Office.Office GetOffice(int? officeId)
        {
            Common.Models.Office.Office office = new Common.Models.Office.Office();

            if (officeId != null && officeId != 0)
            {
                const string sql = "SELECT * FROM office.offices WHERE office_id=@OfficeId;";
                using (NpgsqlCommand command = new NpgsqlCommand(sql))
                {
                    command.Parameters.AddWithValue("@OfficeId", officeId);
                    using (DataTable table = DbOperations.GetDataTable(command))
                    {
                        if (table != null)
                        {
                            if (table.Rows.Count.Equals(1))
                            {
                                office = GetOffice(table.Rows[0]);
                            }
                        }
                    }
                }
            }

            return office;
        }

        private static Common.Models.Office.Office GetOffice(DataRow row)
        {
            Common.Models.Office.Office office = new Common.Models.Office.Office();

            office.OfficeId = Conversion.TryCastInteger(DataRowHelper.GetColumnValue(row, "office_id"));
            office.OfficeCode = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "office_code"));
            office.OfficeName = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "office_name"));
            office.Nickname = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "nick_name"));
            office.RegistrationDate = Conversion.TryCastDate(DataRowHelper.GetColumnValue(row, "registration_date"));
            office.CurrencyCode = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "currency_code"));
            office.AddressLine1 = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "address_line_1"));
            office.AddressLine2 = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "address_line_2"));
            office.Street = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "street"));
            office.City = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "city"));
            office.State = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "state"));
            office.ZipCode = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "zip_code"));
            office.Country = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "country"));
            office.Phone = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "phone"));
            office.Fax = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "fax"));
            office.Email = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "email"));
            office.Url = new Uri(Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "url")), UriKind.RelativeOrAbsolute);
            office.RegistrationNumber = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "registration_number"));
            office.PanNumber = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "pan_number"));
            office.ParentOfficeId = Conversion.TryCastInteger(DataRowHelper.GetColumnValue(row, "parent_office_id"));
            office.ParentOffice = GetOffice(office.ParentOfficeId);

            return office;
        }

        private static Collection<Common.Models.Office.Office> GetOffices(DataTable table)
        {
            Collection<Common.Models.Office.Office> officeCollection = new Collection<Common.Models.Office.Office>();
            if (table == null || table.Rows.Count.Equals(0))
            {
                return officeCollection;
            }

            foreach (DataRow row in table.Rows)
            {
                if (row != null)
                {
                    Common.Models.Office.Office office = GetOffice(row);

                    officeCollection.Add(office);
                }
            }

            return officeCollection;
        }
    }
}