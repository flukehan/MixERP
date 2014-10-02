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

using System.IO;
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
        private Panel reportBody;

        private ReportHeader header;
        private Literal reportTitleLiteral;
        private Literal topSectionLiteral;
        private Literal bodyContentsLiteral;
        private PlaceHolder gridPlaceHolder;
        private Literal bottomSectionLiteral;

        private string reportPath;

        private bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(this.Path))
            {
                return false;
            }

            this.reportPath = this.Page.Server.MapPath(this.Path);

            if (!File.Exists(this.reportPath))
            {
                return false;
            }

            return true;
        }

        private void AddReportBody(Panel container)
        {
            this.reportBody = new Panel();
            this.reportBody.ID = "report";

            if (!NoHeader)
            {
                this.header = new ReportHeader();
                this.header.ResourceAssembly = this.ResourceAssembly;
                this.header.Path = ConfigurationHelper.GetReportParameter("HeaderPath");
                this.reportBody.Controls.Add(this.header);
            }

            this.reportTitleLiteral = new Literal();
            this.reportBody.Controls.Add(this.reportTitleLiteral);

            this.topSectionLiteral = new Literal();
            this.reportBody.Controls.Add(this.topSectionLiteral);

            this.bodyContentsLiteral = new Literal();
            this.reportBody.Controls.Add(this.bodyContentsLiteral);

            this.gridPlaceHolder = new PlaceHolder();
            this.reportBody.Controls.Add(this.gridPlaceHolder);

            this.bottomSectionLiteral = new Literal();
            this.reportBody.Controls.Add(this.bottomSectionLiteral);

            container.Controls.Add(this.reportBody);
        }
    }
}