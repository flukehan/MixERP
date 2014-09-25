using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

[assembly: WebResource("MixERP.Net.WebControls.ReportEngine.Scripts.JsBarcode.CODE128.js", "application/x-javascript")]
[assembly: WebResource("MixERP.Net.WebControls.ReportEngine.Scripts.JsBarcode.CODE39.js", "application/x-javascript")]
[assembly: WebResource("MixERP.Net.WebControls.ReportEngine.Scripts.JsBarcode.EAN_UPC.js", "application/x-javascript")]
[assembly: WebResource("MixERP.Net.WebControls.ReportEngine.Scripts.JsBarcode.ITF.js", "application/x-javascript")]
[assembly: WebResource("MixERP.Net.WebControls.ReportEngine.Scripts.JsBarcode.ITF14.js", "application/x-javascript")]
[assembly: WebResource("MixERP.Net.WebControls.ReportEngine.Scripts.JsBarcode.JsBarcode.js", "application/x-javascript")]
[assembly: WebResource("MixERP.Net.WebControls.ReportEngine.Scripts.ReportEngine.Barcode.js", "application/x-javascript")]
[assembly: WebResource("MixERP.Net.WebControls.ReportEngine.Scripts.JQueryQRcode.jquery.qrcode.js", "application/x-javascript")]
[assembly: WebResource("MixERP.Net.WebControls.ReportEngine.Scripts.JQueryQRcode.qrcode.js", "application/x-javascript")]
[assembly: WebResource("MixERP.Net.WebControls.ReportEngine.Scripts.ReportEngine.QRCode.js", "application/x-javascript")]

namespace MixERP.Net.WebControls.ReportEngine
{
    public partial class Report
    {
        [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
        private void AddJavascript()
        {
            JavaScriptHelper.AddJSReference(this.Page, "MixERP.Net.WebControls.ReportEngine.Scripts.JsBarcode.CODE128.js", "code128", this.GetType());
            JavaScriptHelper.AddJSReference(this.Page, "MixERP.Net.WebControls.ReportEngine.Scripts.JsBarcode.CODE39.js", "code39", this.GetType()); //404 error
            JavaScriptHelper.AddJSReference(this.Page, "MixERP.Net.WebControls.ReportEngine.Scripts.JsBarcode.EAN_UPC.js", "ean_upc", this.GetType());
            JavaScriptHelper.AddJSReference(this.Page, "MixERP.Net.WebControls.ReportEngine.Scripts.JsBarcode.ITF.js", "itf", this.GetType());
            JavaScriptHelper.AddJSReference(this.Page, "MixERP.Net.WebControls.ReportEngine.Scripts.JsBarcode.ITF14.js", "itf14", this.GetType());
            JavaScriptHelper.AddJSReference(this.Page, "MixERP.Net.WebControls.ReportEngine.Scripts.JsBarcode.JsBarcode.js", "jsbarcode", this.GetType());
            JavaScriptHelper.AddJSReference(this.Page, "MixERP.Net.WebControls.ReportEngine.Scripts.ReportEngine.Barcode.js", "reportengine_barcode", this.GetType());
            JavaScriptHelper.AddJSReference(this.Page, "MixERP.Net.WebControls.ReportEngine.Scripts.JQueryQRcode.jquery.qrcode.js", "reportengine_jquery_qrcode", this.GetType());
            JavaScriptHelper.AddJSReference(this.Page, "MixERP.Net.WebControls.ReportEngine.Scripts.JQueryQRcode.qrcode.js", "reportengine_qrcode1", this.GetType());
            JavaScriptHelper.AddJSReference(this.Page, "MixERP.Net.WebControls.ReportEngine.Scripts.ReportEngine.QRCode.js", "reportengine_qrcode2", this.GetType());
        }
    }
}