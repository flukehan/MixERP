using System.IO;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Helpers;
/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.WebControls.ScrudFactory.Resources;

namespace MixERP.Net.WebControls.ScrudFactory.Controls
{
    public static class ScrudFileUpload
    {
        public static void AddFileUpload(HtmlTable htmlTable, string resourceClassName, string columnName, bool isNullable)
        {
            var label = LocalizationHelper.GetResourceString(resourceClassName, columnName);
            var fileUpload = GetFileUpload(columnName + "_fileupload");
            var validator = GetImageValidator(fileUpload);

            //Todo: One of the following:
            //1. Ask the script manager to do a synchronous postback on save button click event.
            //2. Implement a handler to upload image using AJAX. 

            if (!isNullable)
            {
                var required = ScrudFactoryHelper.GetRequiredFieldValidator(fileUpload);
                ScrudFactoryHelper.AddRow(htmlTable, label + ScrudResource.RequiredFieldIndicator, fileUpload, required, validator);
                return;
            }

            ScrudFactoryHelper.AddRow(htmlTable, label, fileUpload, validator);
        }

        public static string UploadFile(FileUpload fileUpload)
        {
            if (fileUpload == null)
            {
                return string.Empty;
            }

            var tempMediaPath = ConfigurationHelper.GetScrudParameter("TempMediaPath");
            var uploadDirectory = HttpContext.Current.Server.MapPath(tempMediaPath);

            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            var id = Guid.NewGuid().ToString();

            if (fileUpload.HasFile)
            {
                id += Path.GetExtension(fileUpload.FileName);
                id = Path.Combine(uploadDirectory, id);

                fileUpload.SaveAs(id);
            }

            return id;
        }

        private static FileUpload GetFileUpload(string id)
        {
            using (var fileUpload = new FileUpload())
            {
                fileUpload.ID = id;

                return fileUpload;
            }
        }

        private static RegularExpressionValidator GetImageValidator(Control controlToValidate)
        {
            using (var validator = new RegularExpressionValidator())
            {
                validator.ID = controlToValidate.ID + "RegexValidator";
                validator.ErrorMessage = @"<br/>" + ScrudResource.InvalidImage;
                validator.CssClass = "form-error";
                validator.ControlToValidate = controlToValidate.ID;
                validator.EnableClientScript = true;
                validator.SetFocusOnError = true;
                validator.Display = ValidatorDisplay.Dynamic;
                validator.ValidationExpression = @"(.*\.([gG][iI][fF]|[jJ][pP][gG]|[jJ][pP][eE][gG]|[bB][mM][pP])$)";
                return validator;
            }
        }

    }
}
