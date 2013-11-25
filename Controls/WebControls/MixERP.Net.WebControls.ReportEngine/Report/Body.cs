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
using System.Web.UI.WebControls;

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

            if(!System.IO.File.Exists(this.reportPath))
            {
                return false;
            }

            return true;
        }



        private void AddReportBody(Panel container)
        {
            reportBody = new Panel();
            reportBody.ID = "report";

            header = new ReportHeader();
            header.Path = MixERP.Net.Common.Helpers.ConfigurationHelper.GetReportParameter( "HeaderPath");                
            reportBody.Controls.Add(header);

            reportTitleLiteral = new Literal();
            reportBody.Controls.Add(reportTitleLiteral);

            topSectionLiteral = new Literal();
            reportBody.Controls.Add(topSectionLiteral);
            
            gridPlaceHolder = new PlaceHolder();
            reportBody.Controls.Add(gridPlaceHolder);
            
            bodyContentsLiteral  = new Literal();
            reportBody.Controls.Add(bodyContentsLiteral);
            
            bottomSectionLiteral = new Literal();
            reportBody.Controls.Add(bottomSectionLiteral);
           
            container.Controls.Add(reportBody);
        }

    }
}
