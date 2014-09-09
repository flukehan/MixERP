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

using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Resources;
using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory.Controls
{
    public static class ScrudFileUpload
    {
        public static void AddFileUpload(HtmlTable htmlTable, string resourceClassName, string columnName,
            bool isNullable, string errorCssClass)
        {
            var label = LocalizationHelper.GetResourceString(resourceClassName, columnName);
            var fileUpload = GetFileUpload(columnName + "_fileupload");
            var validator = GetImageValidator(fileUpload, errorCssClass);

            //Todo: One of the following:
            //1. Ask the script manager to do a synchronous postback on save button click event.
            //2. Implement a handler to upload image using AJAX.

            if (!isNullable)
            {
                var required = ScrudFactoryHelper.GetRequiredFieldValidator(fileUpload, errorCssClass);
                ScrudFactoryHelper.AddRow(htmlTable, label + ScrudResource.RequiredFieldIndicator, fileUpload, required,
                    validator);
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

        private static RegularExpressionValidator GetImageValidator(Control controlToValidate, string cssClass)
        {
            using (var validator = new RegularExpressionValidator())
            {
                validator.ID = controlToValidate.ID + "RegexValidator";
                validator.ErrorMessage = @"<br/>" + ScrudResource.InvalidImage;
                validator.CssClass = cssClass;
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