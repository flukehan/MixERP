/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace MixERP.Net.WebControls.ScrudFactory.Helpers
{
    public class AjaxProgressTemplate : ITemplate
    {
        private readonly string cssClass;
        private readonly string spinnerImageCssClass;
        private readonly string spinnerImagePath;


        public AjaxProgressTemplate(string cssClass, string spinnerImageCssClass, string spinnerImagePath)
        {
            this.cssClass = cssClass;
            this.spinnerImageCssClass = spinnerImageCssClass;
            this.spinnerImagePath = spinnerImagePath;
        }


        public void InstantiateIn(Control container)
        {
            using (var ajaxProgressDiv = new HtmlGenericControl("div"))
            {
                ajaxProgressDiv.Attributes.Add("class", this.cssClass);

                using (var ajaxImage = new HtmlGenericControl("img"))
                {
                    ajaxImage.Attributes.Add("src", this.spinnerImagePath);
                    ajaxImage.Attributes.Add("class", this.spinnerImageCssClass);

                    ajaxProgressDiv.Controls.Add(ajaxImage);

                    container.Controls.Add(ajaxProgressDiv);
                }
            }
        }
    }

}
