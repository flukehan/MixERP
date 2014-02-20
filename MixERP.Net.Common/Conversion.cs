/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.ComponentModel;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Web.Configuration;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.Text;
    using System.Security.Cryptography;
    using System.Web;
    using Models.Transactions;
    using System.Web.UI;

    public static class Conversion
    {

        public static string GetBookAcronym(TranBook book, SubTranBook subBook)
        {
            if (book == TranBook.Sales)
            {
                if (subBook == SubTranBook.Delivery)
                {
                    return ConfigurationHelper.GetParameter("SalesDeliveryAcronym");
                }

                if (subBook == SubTranBook.Direct)
                {
                    return ConfigurationHelper.GetParameter("SalesDirectAcronym");
                }

                if (subBook == SubTranBook.Invoice)
                {
                    return ConfigurationHelper.GetParameter("SalesInvoiceAcronym");
                }

                if (subBook == SubTranBook.Order)
                {
                    return ConfigurationHelper.GetParameter("SalesOrderAcronym");
                }

                if (subBook == SubTranBook.Quotation)
                {
                    return ConfigurationHelper.GetParameter("SalesQuotationAcronym");
                }

                if (subBook == SubTranBook.Receipt)
                {
                    return ConfigurationHelper.GetParameter("SalesReceiptAcronym");
                }

                if (subBook == SubTranBook.Return)
                {
                    return ConfigurationHelper.GetParameter("SaleReturnAcronym");
                }
            }

            if (book == TranBook.Purchase)
            {
                if (subBook == SubTranBook.Direct)
                {
                    return ConfigurationHelper.GetParameter("PurchaseDirectAcronym");
                }

                if (subBook == SubTranBook.Order)
                {
                    return ConfigurationHelper.GetParameter("PurchaseOrderAcronym");
                }

                if (subBook == SubTranBook.Payment)
                {
                    return ConfigurationHelper.GetParameter("PurchasePaymentAcronym");
                }

                if (subBook == SubTranBook.Receipt)
                {
                    return ConfigurationHelper.GetParameter("PurchaseGRNAcronym");
                }

                if (subBook == SubTranBook.Return)
                {
                    return ConfigurationHelper.GetParameter("PurchaseReturnAcronym");
                }
            }

            return string.Empty;
        }

        public static string ResolveUrl(string url)
        {
            Page page = HttpContext.Current.Handler as Page;

            if (page == null)
            {
                return url;
            }

            return (page).ResolveUrl(url);
        }

        public static string MapPathReverse(string fullServerPath)
        {
            if (string.IsNullOrWhiteSpace(fullServerPath))
            {
                return null;
            }
            string physicalApplicationPath = HttpContext.Current.Request.PhysicalApplicationPath;

            if (string.IsNullOrWhiteSpace(physicalApplicationPath))
            {
                return null;
            }

            return @"~/" + fullServerPath.Replace(physicalApplicationPath, String.Empty).Replace(@"\", "/");
        }

        public static string GetRelativePath(string absolutePath)
        {
            if (string.IsNullOrWhiteSpace(absolutePath))
            {
                return null;
            }

            string physicalPath = HttpContext.Current.Request.MapPath(absolutePath);

            return MapPathReverse(physicalPath);
        }

        public static short TryCastShort(object value)
        {
            short retVal = 0;

            if (value != null)
            {
                //string numberToParse = RemoveGroupping(value.ToString());
                string numberToParse = value.ToString();

                if (short.TryParse(numberToParse, out retVal))
                {
                    return retVal;
                }
            }

            return retVal;
        }

        public static long TryCastLong(object value)
        {
            long retVal = 0;

            if (value != null)
            {
                //string numberToParse = RemoveGroupping(value.ToString());
                string numberToParse = value.ToString();

                if (long.TryParse(numberToParse, out retVal))
                {
                    return retVal;
                }
            }

            return retVal;
        }

        public static float TryCastSingle(object value)
        {
            float retVal = 0;

            if (value != null)
            {
                //string numberToParse = RemoveGroupping(value.ToString());
                string numberToParse = value.ToString();

                if (float.TryParse(numberToParse, out retVal))
                {
                    return retVal;
                }
            }

            return retVal;
        }

        public static double TryCastDouble(object value)
        {
            double retVal = 0;

            if (value != null)
            {
                //string numberToParse = RemoveGroupping(value.ToString());
                string numberToParse = value.ToString();

                if (double.TryParse(numberToParse, out retVal))
                {
                    return retVal;
                }
            }

            return retVal;
        }

        public static int TryCastInteger(object value)
        {
            int retVal = 0;

            if (value != null)
            {
                if (value is bool)
                {
                    if (Convert.ToBoolean(value, CultureInfo.InvariantCulture))
                    {
                        return 1;
                    }
                }

                //string numberToParse = RemoveGroupping(value.ToString());
                string numberToParse = value.ToString();

                if (int.TryParse(numberToParse, out retVal))
                {
                    return retVal;
                }
            }

            return retVal;
        }

        public static DateTime TryCastDate(object value)
        {
            try
            {
                if (value == DBNull.Value)
                {
                    return DateTime.MinValue;
                }

                return Convert.ToDateTime(value, Thread.CurrentThread.CurrentCulture);
            }
            catch (FormatException)
            {
                //swallow the exception
            }
            catch (InvalidCastException)
            {
                //swallow the exception
            }

            return DateTime.MinValue;
        }

        //private static string RemoveGroupping(string number)
        //{
        //    string thousandSeparator = Helpers.Parameters.ThousandSeparator();
        //    string decimalSeparator = Helpers.Parameters.DecimalSeparator();

        //    //Remove the thousand separator from the number
        //    number = number.Replace(thousandSeparator, "");

        //    //Replace the decimal separator with "dot".
        //    if(!decimalSeparator.Equals("."))
        //    {
        //        number = number.Replace(decimalSeparator, ".");
        //    }

        //    return number;
        //}

        public static decimal TryCastDecimal(object value)
        {
            decimal retVal = 0;

            if (value != null)
            {
                //string numberToParse = RemoveGroupping(value.ToString());
                string numberToParse = value.ToString();

                if (decimal.TryParse(numberToParse, out retVal))
                {
                    return retVal;
                }
            }

            return retVal;
        }

        public static bool TryCastBoolean(object value)
        {
            bool retVal = false;

            if (value != null)
            {
                if (value is string)
                {
                    if (value.ToString().ToLower(Thread.CurrentThread.CurrentCulture).Equals("yes"))
                    {
                        return true;
                    }

                    if (value.ToString().ToLower(Thread.CurrentThread.CurrentCulture).Equals("true"))
                    {
                        return true;
                    }
                }

                if (bool.TryParse(value.ToString(), out retVal))
                {
                    return retVal;
                }
            }

            return retVal;
        }

        public static bool IsNumeric(string value)
        {
            double number;
            return double.TryParse(value, out number);
        }

        public static string TryCastString(object value)
        {
            try
            {
                if (value != null)
                {
                    if (value is bool)
                    {
                        if (Convert.ToBoolean(value, CultureInfo.InvariantCulture))
                        {
                            return "true";
                        }

                        return "false";
                    }

                    if (value == DBNull.Value)
                    {
                        return string.Empty;
                    }

                    string retVal = value.ToString();
                    return retVal;
                }

                return string.Empty;
            }
            catch (FormatException)
            {
                //swallow the exception
            }
            catch (InvalidCastException)
            {
                //swallow the exception            
            }

            return string.Empty;
        }

        public static string HashSha512(string password, string salt)
        {
            if (password == null)
            {
                return null;
            }

            if (salt == null)
            {
                return null;
            }

            byte[] bytes = Encoding.Unicode.GetBytes(password + salt);
            using (SHA512CryptoServiceProvider hash = new SHA512CryptoServiceProvider())
            {
                byte[] inArray = hash.ComputeHash(bytes);
                return Convert.ToBase64String(inArray);
            }
        }


        public static Uri GetBackEndUrl(HttpContext context, string relativePath)
        {
            string administrationDirectoryName = WebConfigurationManager.AppSettings["AdministrationDirectoryName"];

            if (context != null)
            {
                if (!string.IsNullOrWhiteSpace(administrationDirectoryName))
                {
                    string lang;

                    if ((context.Session == null) || (context.Session["lang"] == null || string.IsNullOrWhiteSpace(context.Session["lang"] as string)))
                    {
                        lang = "en-US";
                    }
                    else
                    {
                        lang = context.Session["lang"] as string;
                    }

                    CultureInfo culture = new CultureInfo(lang);
                    if (culture.TwoLetterISOLanguageName == "iv")
                    {
                        culture = new CultureInfo("en-US");
                    }

                    string virtualDirectory = context.Request.ApplicationPath;
                    bool isSecure = context.Request.IsSecureConnection;
                    string domain = context.Request.Url.DnsSafeHost;
                    int port = context.Request.Url.Port;
                    string path;

                    if (virtualDirectory == "/")
                    {
                        path = string.Format(CultureInfo.InvariantCulture, "{0}:{1}/{2}/{3}/{4}/", domain, port.ToString(CultureInfo.InvariantCulture), administrationDirectoryName, culture.TwoLetterISOLanguageName, relativePath);
                    }
                    else
                    {
                        path = string.Format(CultureInfo.InvariantCulture, "{0}:{1}{2}/{3}/{4}/{5}/", domain, port.ToString(CultureInfo.InvariantCulture), virtualDirectory, administrationDirectoryName, culture.TwoLetterISOLanguageName, relativePath);
                    }

                    if (isSecure)
                    {
                        path = "https://" + path;
                    }
                    else
                    {
                        path = "http://" + path;
                    }

                    return new Uri(path, UriKind.Absolute);
                }
            }
            return new Uri("", UriKind.Relative);
        }

        public static DateTime GetLocalDateTime(string timeZone, DateTime utc)
        {
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            return TimeZoneInfo.ConvertTimeFromUtc(utc, zone);
        }

        public static string GetLocalDateTimeString(string timeZone, DateTime utc)
        {
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            DateTime time = TimeZoneInfo.ConvertTimeFromUtc(utc, zone);
            return time.ToLongDateString() + " " + time.ToLongTimeString() + " " + zone.DisplayName;
        }

        public static byte[] TryCastByteArray(Bitmap bitmap)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
        }

        public static byte[] TryCastByteArray(Image image)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(image, typeof(byte[]));
        }

        public static DataTable ConvertListToDataTable<T>(IList<T> list)
        {
            if (list == null)
            {
                return null;
            }

            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

            using (DataTable table = new DataTable())
            {
                table.Locale = Thread.CurrentThread.CurrentCulture;

                for (int i = 0; i < props.Count; i++)
                {
                    PropertyDescriptor prop = props[i];
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }
                object[] values = new object[props.Count];
                foreach (T item in list)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = props[i].GetValue(item);
                    }
                    table.Rows.Add(values);
                }
                return table;
            }
        }

        public static byte[] ConvertImageToByteArray(Image imageToConvert, ImageFormat formatOfImage)
        {
            if (imageToConvert == null)
            {
                return null;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                imageToConvert.Save(ms, formatOfImage);
                return ms.ToArray();
            }
        }
    }
}
