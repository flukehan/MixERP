using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Serilog;

namespace MixERP.Net.FrontEnd.Services
{

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Install : System.Web.Services.WebService
    {

        [WebMethod]
        public bool SaveOffice(string officeCode, string officeName, string nickName, string registrationDate, string currencyCode, string currencySymbol, string currencyName, string hundredthName, string adminName, string userName, string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(officeName) || string.IsNullOrWhiteSpace(officeCode) || string.IsNullOrWhiteSpace(nickName) || string.IsNullOrWhiteSpace(registrationDate) ||string.IsNullOrWhiteSpace(currencyCode) || string.IsNullOrWhiteSpace(currencySymbol) || string.IsNullOrWhiteSpace(currencyName) || string.IsNullOrWhiteSpace(hundredthName) || string.IsNullOrWhiteSpace(adminName) || string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                return false;
            }

            if (password != confirmPassword)
            {
                return false;
            }

            try
            {
                return Data.Office.Offices.SaveOffice(officeCode, officeName, nickName, Convert.ToDateTime(registrationDate), currencyCode,
                    currencySymbol, currencyName, hundredthName, adminName, userName, password);
            }
            catch (Exception ex)
            {
                Log.Warning("Could not save office. {Exception}", ex);
                throw;
            }
        }
    }
}
