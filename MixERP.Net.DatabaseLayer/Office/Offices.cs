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
using Npgsql;
using System.Collections.ObjectModel;

namespace MixERP.Net.DatabaseLayer.Office
{
    public static class Offices
    {
        public static MixERP.Net.Common.Models.Office.Office GetOffice(int? officeId)
        {
            MixERP.Net.Common.Models.Office.Office office = new Common.Models.Office.Office();

            if (officeId != null || officeId != 0)
            {
                string sql = "SELECT * FROM office.offices WHERE office_id=@OfficeId;";
                using (NpgsqlCommand command = new NpgsqlCommand(sql))
                {
                    command.Parameters.Add("@OfficeId", officeId);
                    using (DataTable table = MixERP.Net.DBFactory.DBOperations.GetDataTable(command))
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

        public static MixERP.Net.Common.Models.Office.Office GetOffice(DataRow row)
        {
            MixERP.Net.Common.Models.Office.Office office = new Common.Models.Office.Office();

            office.OfficeId = MixERP.Net.Common.Conversion.TryCastInteger(Helpers.ConversionHelper.GetColumnValue(row, "office_id"));
            office.OfficeCode = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "office_code"));
            office.OfficeName = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "office_name"));
            office.NickName = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "nick_name"));
            office.RegistrationDate = MixERP.Net.Common.Conversion.TryCastDate(Helpers.ConversionHelper.GetColumnValue(row, "registration_date"));
            office.CurrencyCode = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "currency_code"));
            office.AddressLine1 = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "address_line_1"));
            office.AddressLine2 = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "address_line_2"));
            office.Street = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "street"));
            office.City = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "city"));
            office.State = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "state"));
            office.ZipCode = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "zip_code"));
            office.Country = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "country"));
            office.Phone = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "phone"));
            office.Fax = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "fax"));
            office.Email = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "email"));
            office.Url = new Uri(MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "url")), UriKind.RelativeOrAbsolute);
            office.RegistrationNumber = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "registration_number"));
            office.PanNumber = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "pan_number"));
            office.ParentOfficeId = MixERP.Net.Common.Conversion.TryCastInteger(Helpers.ConversionHelper.GetColumnValue(row, "parent_office_id"));
            office.ParentOffice = GetOffice(office.ParentOfficeId);

            return office;
        }

        public static Collection<MixERP.Net.Common.Models.Office.Office> GetOffices()
        {
            string sql = "SELECT * FROM office.get_offices();";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                return GetOffices(MixERP.Net.DBFactory.DBOperations.GetDataTable(command));
            }
        }

        private static Collection<MixERP.Net.Common.Models.Office.Office> GetOffices(DataTable table)
        {
            Collection<MixERP.Net.Common.Models.Office.Office> officeCollection = new Collection<Common.Models.Office.Office>();
            if (table == null || table.Rows.Count.Equals(0))
            {
                return officeCollection;
            }

            foreach (DataRow row in table.Rows)
            {
                if (row != null)
                {
                    MixERP.Net.Common.Models.Office.Office office = GetOffice(row);

                    officeCollection.Add(office);
                }
            }

            return officeCollection;

        }

    }
}
