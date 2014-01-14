/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.WebControls.ScrudFactory.Helpers;
using System.Web;

namespace MixERP.Net.WebControls.ScrudFactory.Controls
{
    public static class ScrudFileUpload
    {
        public static void AddFileUpload(HtmlTable htmlTable, string resourceClassName, string columnName, bool isNullable)
        {
            string label = MixERP.Net.Common.Helpers.LocalizationHelper.GetResourceString(resourceClassName, columnName);
            FileUpload fileUpload = GetFileUpload(columnName + "_fileupload");
            RegularExpressionValidator validator = GetImageValidator(fileUpload);

            //Todo: One of the following:
            //1. Ask the script manager to do a synchronous postback on save button click event.
            //2. Implement a handler to upload image using AJAX. 

            if (!isNullable)
            {
                RequiredFieldValidator required = ScrudFactoryHelper.GetRequiredFieldValidator(fileUpload);
                ScrudFactoryHelper.AddRow(htmlTable, label + Resources.ScrudResource.RequiredFieldIndicator, fileUpload, required, validator);
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

            string tempMediaPath = MixERP.Net.Common.Helpers.ConfigurationHelper.GetScrudParameter("TempMediaPath");
            string uploadDirectory = HttpContext.Current.Server.MapPath(tempMediaPath);

            if (!System.IO.Directory.Exists(uploadDirectory))
            {
                System.IO.Directory.CreateDirectory(uploadDirectory);
            }

            string id = Guid.NewGuid().ToString();

            if (fileUpload.HasFile)
            {
                id += System.IO.Path.GetExtension(fileUpload.FileName);
                id = System.IO.Path.Combine(uploadDirectory, id);

                fileUpload.SaveAs(id);
            }

            return id;
        }

        private static FileUpload GetFileUpload(string id)
        {
            using (FileUpload fileUpload = new FileUpload())
            {
                fileUpload.ID = id;

                return fileUpload;
            }
        }

        private static RegularExpressionValidator GetImageValidator(Control controlToValidate)
        {
            using (RegularExpressionValidator validator = new RegularExpressionValidator())
            {
                validator.ID = controlToValidate.ID + "RegexValidator";
                validator.ErrorMessage = "<br/>" + Resources.ScrudResource.InvalidImage;
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
