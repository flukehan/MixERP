using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Office;
using MixERP.Net.DBFactory;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace MixERP.Net.FrontEnd.Data.Office
{
    public static class User
    {
        public static bool SignIn(int officeId, string userName, string password, string culture, bool remember, Page page)
        {
            if (page != null)
            {
                try
                {
                    string remoteAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    string remoteUser = HttpContext.Current.Request.ServerVariables["REMOTE_USER"];

                    long logOnId = SignIn(officeId, userName, Conversion.HashSha512(password, userName), page.Request.UserAgent, remoteAddress, remoteUser, culture);

                    if (logOnId > 0)
                    {
                        return true;
                    }
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch
                {
                    //Swallow the exception here
                }
            }

            return false;
        }

        private static long SignIn(int officeId, string userName, string password, string browser, string remoteAddress, string remoteUser, string culture)
        {
            const string sql = "SELECT * FROM office.sign_in(@OfficeId, @UserName, @Password, @Browser, @IPAddress, @RemoteUser, @Culture);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Browser", browser);
                command.Parameters.AddWithValue("@IPAddress", remoteAddress);
                command.Parameters.AddWithValue("@RemoteUser", remoteUser);
                command.Parameters.AddWithValue("@Culture", culture);

                return Conversion.TryCastLong(DbOperations.GetScalarValue(command));
            }
        }

        public static SignInView GetLastSignInView(string userName)
        {
            SignInView view = new SignInView();

            const string sql = "SELECT * FROM office.sign_in_view WHERE user_name=@UserName;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@UserName", userName);

                using (DataTable table = DbOperations.GetDataTable(command))
                {
                    if (table != null && table.Rows.Count.Equals(1))
                    {
                        view = GetSignInView(table.Rows[0]);
                    }
                }
            }

            return view;
        }

        public static SignInView GetSignInView(DataRow row)
        {
            SignInView view = new SignInView();

            view.UserId = Conversion.TryCastInteger(DataRowHelper.GetColumnValue(row, "user_id"));
            view.Role = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "role"));
            view.IsAdmin = Conversion.TryCastBoolean(DataRowHelper.GetColumnValue(row, "is_admin"));
            view.IsSystem = Conversion.TryCastBoolean(DataRowHelper.GetColumnValue(row, "is_system"));
            view.UserName = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "user_name"));
            view.FullName = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "full_name"));
            view.LogOnId = Conversion.TryCastInteger(DataRowHelper.GetColumnValue(row, "login_id"));
            view.OfficeId = Conversion.TryCastInteger(DataRowHelper.GetColumnValue(row, "office_id"));
            view.Culture = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "culture"));
            view.Office = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "office"));
            view.OfficeCode = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "office_code"));
            view.OfficeName = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "office_name"));
            view.Nickname = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "nick_name"));
            view.RegistrationDate = Conversion.TryCastDate(DataRowHelper.GetColumnValue(row, "registration_date"));
            view.RegistrationNumber = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "registration_number"));
            view.PanNumber = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "pan_number"));
            view.AddressLine1 = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "address_line_1"));
            view.AddressLine2 = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "address_line_2"));
            view.Street = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "street"));
            view.City = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "city"));
            view.State = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "state"));
            view.ZipCode = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "zip_code"));
            view.Country = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "country"));
            view.Phone = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "phone"));
            view.Fax = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "fax"));
            view.Email = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "email"));
            view.Url = new Uri(Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "url")), UriKind.RelativeOrAbsolute);

            return view;
        }
    }
}