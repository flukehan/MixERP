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

using System;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Finance.Data;
using MixERP.Net.Entities.Contracts;
using MixERP.Net.FrontEnd.Base;

namespace MixERP.Net.Core.Modules.Finance
{
    public partial class EODOperation : MixERPUserControl, ITransaction
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.Initialize();
        }

        private new void Initialize()
        {
            int officeId = CurrentSession.GetOfficeId();

            EODStatus status = Data.EODOperation.GetStatus(officeId);

            if (status != null)
            {
                this.ValueDateLiteral.Text = @"(" + status.ValueDate.ToShortDateString() + @")";

                if (status.Initialized)
                {
                    this.InitializeButton.Attributes.Add("class", "ui blue disabled button");
                    this.PerformEODButton.Attributes.Add("class", "ui red button");
                    return;
                }

                this.InitializeButton.Attributes.Add("class", "ui blue button");
                this.PerformEODButton.Attributes.Add("class", "ui red disabled button");
            }
        }
    }
}