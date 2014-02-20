/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.IO;
using System.Web.UI.WebControls;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.WebControls.ReportEngine
{
    public partial class Report
    {
        /// <summary>
        /// ReportBody is displayed below the CommandPanel and contains the following sections:
        /// 1. Report Header
        /// 2. Report Title
        /// 3. Top Section
        /// 4. Report Details (Grid)
        /// 5. Bottom Section
        /// </summary>
        Panel reportBody;
        ReportHeader header;
        Literal reportTitleLiteral;
        Literal topSectionLiteral;
        Literal bodyContentsLiteral;
        PlaceHolder gridPlaceHolder;
        Literal bottomSectionLiteral;

        private string reportPath;
        private bool IsValid()
        {
            if(string.IsNullOrWhiteSpace(this.Path))
            {
                return false;
            }

            this.reportPath = this.Page.Server.MapPath(this.Path);

            if(!File.Exists(this.reportPath))
            {
                return false;
            }

            return true;
        }



        private void AddReportBody(Panel container)
        {
            this.reportBody = new Panel();
            this.reportBody.ID = "report";

            this.header = new ReportHeader();
            this.header.Path = ConfigurationHelper.GetReportParameter( "HeaderPath");                
            this.reportBody.Controls.Add(this.header);

            this.reportTitleLiteral = new Literal();
            this.reportBody.Controls.Add(this.reportTitleLiteral);

            this.topSectionLiteral = new Literal();
            this.reportBody.Controls.Add(this.topSectionLiteral);
            
            this.gridPlaceHolder = new PlaceHolder();
            this.reportBody.Controls.Add(this.gridPlaceHolder);
            
            this.bodyContentsLiteral  = new Literal();
            this.reportBody.Controls.Add(this.bodyContentsLiteral);
            
            this.bottomSectionLiteral = new Literal();
            this.reportBody.Controls.Add(this.bottomSectionLiteral);
           
            container.Controls.Add(this.reportBody);
        }

    }
}
