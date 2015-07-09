using MixERP.Net.ApplicationState;
using MixERP.Net.Common;
using MixERP.Net.DbFactory;
using Npgsql;
using System;
using System.Collections.ObjectModel;
using System.Data;

namespace MixERP.Net.WebControls.Common.Data
{
    public static class Frequency
    {
        public static FrequencyDates GetFrequencyDates(string catalog, int officeId)
        {
            const string sql = "SELECT @OfficeId AS office_id, core.get_date(@OfficeId::integer) AS today, core.get_month_start_date(@OfficeId::integer) AS month_start_date,core.get_month_end_date(@OfficeId::integer) AS month_end_date, core.get_quarter_start_date(@OfficeId::integer) AS quarter_start_date, core.get_quarter_end_date(@OfficeId::integer) AS quarter_end_date, core.get_fiscal_half_start_date(@OfficeId::integer) AS fiscal_half_start_date, core.get_fiscal_half_end_date(@OfficeId::integer) AS fiscal_half_end_date, core.get_fiscal_year_start_date(@OfficeId::integer) AS fiscal_year_start_date, core.get_fiscal_year_end_date(@OfficeId::integer) AS fiscal_year_end_date;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);

                using (DataTable table = DbOperation.GetDataTable(catalog, command))
                {
                    if (table != null && table.Rows != null && table.Rows.Count.Equals(1))
                    {
                        return GetApplicationDateModel(table.Rows[0]);
                    }
                }
            }

            return null;
        }

        public static Collection<FrequencyDates> GetFrequencyDates(string catalog)
        {
            Collection<FrequencyDates> applicationDates = new Collection<FrequencyDates>();

            const string sql = "SELECT office_id AS office_id, core.get_date(office_id) AS today, core.get_month_start_date(office_id) AS month_start_date,core.get_month_end_date(office_id) AS month_end_date, core.get_quarter_start_date(office_id) AS quarter_start_date, core.get_quarter_end_date(office_id) AS quarter_end_date, core.get_fiscal_half_start_date(office_id) AS fiscal_half_start_date, core.get_fiscal_half_end_date(office_id) AS fiscal_half_end_date, core.get_fiscal_year_start_date(office_id) AS fiscal_year_start_date, core.get_fiscal_year_end_date(office_id) AS fiscal_year_end_date FROM office.offices;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                using (DataTable table = DbOperation.GetDataTable(catalog, command))
                {
                    if (table != null && table.Rows != null && table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            applicationDates.Add(GetApplicationDateModel(row));
                        }
                    }
                }
            }

            return applicationDates;
        }

        public static DateTime GetDate(string catalog, int officeId)
        {
            const string sql = "SELECT core.get_date(@OfficeId::integer);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);
                return Conversion.TryCastDate(DbOperation.GetScalarValue(catalog, command));
            }
        }

        public static DateTime GetFiscalHalfEndDate(string catalog, int officeId)
        {
            const string sql = "SELECT core.get_fiscal_half_end_date(@OfficeId::integer);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);
                return Conversion.TryCastDate(DbOperation.GetScalarValue(catalog, command));
            }
        }

        public static DateTime GetFiscalHalfStartDate(string catalog, int officeId)
        {
            const string sql = "SELECT core.get_fiscal_half_start_date(@OfficeId::integer);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);
                return Conversion.TryCastDate(DbOperation.GetScalarValue(catalog, command));
            }
        }

        public static DateTime GetFiscalYearEndDate(string catalog, int officeId)
        {
            const string sql = "SELECT core.get_fiscal_year_end_date(@OfficeId::integer);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);
                return Conversion.TryCastDate(DbOperation.GetScalarValue(catalog, command));
            }
        }

        public static DateTime GetFiscalYearStartDate(string catalog, int officeId)
        {
            const string sql = "SELECT core.get_fiscal_year_start_date(@OfficeId::integer);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);
                return Conversion.TryCastDate(DbOperation.GetScalarValue(catalog, command));
            }
        }

        public static DateTime GetMonthEndtDate(string catalog, int officeId)
        {
            const string sql = "SELECT core.get_month_end_date(@OfficeId::integer);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);
                return Conversion.TryCastDate(DbOperation.GetScalarValue(catalog, command));
            }
        }

        public static DateTime GetMonthStartDate(string catalog, int officeId)
        {
            const string sql = "SELECT core.get_month_start_date(@OfficeId::integer);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);
                return Conversion.TryCastDate(DbOperation.GetScalarValue(catalog, command));
            }
        }

        public static DateTime GetQuarterEndDate(string catalog, int officeId)
        {
            const string sql = "SELECT core.get_quarter_end_date(@OfficeId::integer);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);
                return Conversion.TryCastDate(DbOperation.GetScalarValue(catalog, command));
            }
        }

        public static DateTime GetQuarterStartDate(string catalog, int officeId)
        {
            const string sql = "SELECT core.get_quarter_start_date(@OfficeId::integer);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);
                return Conversion.TryCastDate(DbOperation.GetScalarValue(catalog, command));
            }
        }

        private static FrequencyDates GetApplicationDateModel(DataRow row)
        {
            if (row == null)
            {
                return null;
            }

            FrequencyDates applicationDate = new FrequencyDates();
            applicationDate.OfficeId = Conversion.TryCastInteger(row["office_id"]);
            applicationDate.Today = Conversion.TryCastDate(row["today"]);
            applicationDate.MonthStartDate = Conversion.TryCastDate(row["month_start_date"]);
            applicationDate.MonthEndDate = Conversion.TryCastDate(row["month_end_date"]);
            applicationDate.QuarterStartDate = Conversion.TryCastDate(row["quarter_start_date"]);
            applicationDate.QuarterEndDate = Conversion.TryCastDate(row["quarter_end_date"]);
            applicationDate.FiscalHalfStartDate = Conversion.TryCastDate(row["fiscal_half_start_date"]);
            applicationDate.FiscalHalfEndDate = Conversion.TryCastDate(row["fiscal_half_end_date"]);
            applicationDate.FiscalYearStartDate = Conversion.TryCastDate(row["fiscal_year_start_date"]);
            applicationDate.FiscalYearEndDate = Conversion.TryCastDate(row["fiscal_year_end_date"]);

            return applicationDate;
        }
    }
}