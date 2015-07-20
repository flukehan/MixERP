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

using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Entities.Contracts;
using MixERP.Net.Framework.Controls;
using Serilog;
using System;

namespace MixERP.Net.FrontEnd.Base
{
    public abstract class MixERPUserControl : MixERPUserControlBase
    {
        public virtual bool IsLandingPage { get; set; }

        public override AccessLevel AccessLevel
        {
            get { return AccessLevel.PolicyBased; }
        }

        public bool IsRestrictedMode
        {
            get { return !AppUsers.GetCurrent().View.AllowTransactionPosting.ToBool(); }
        }

        public void Initialize()
        {
            Log.Verbose("{Control} initialized.", this);
            this.OnControlLoad(this, new EventArgs());

            this.CheckAccessLevel();
            this.CheckTransactionPostingStatus();
        }

        private void CheckTransactionPostingStatus()
        {
            if (this is ITransaction)
            {
                if (!AppUsers.GetCurrent().View.AllowTransactionPosting.ToBool())
                {
                    this.Server.Transfer("~/Site/Exceptions/RestrictedTransactionMode.aspx");
                }
            }
        }

        private void CheckAccessLevel()
        {
            var login = AppUsers.GetCurrent();
            bool hasAccess = true;
            string userName = string.Empty;
            string ipAddress = string.Empty;

            if (login == null || login.View == null)
            {
                hasAccess = false;
            }
            else
            {
                userName = AppUsers.GetCurrent().View.UserName;
                ipAddress = AppUsers.GetCurrent().View.IpAddress;

                bool isDevelopmentMode =
                    DbConfig.GetMixERPParameter(AppUsers.GetCurrentUserDB(), "Mode")
                        .ToUpperInvariant()
                        .Equals("DEVELOPMENT");
                bool isLocalHost = PageUtility.IsLocalhost(this.Page);
                bool adminOnly = (this.AccessLevel.Equals(AccessLevel.AdminOnly) ||
                                  this.AccessLevel.Equals(AccessLevel.LocalhostAdmin));


                if (adminOnly)
                {
                    hasAccess = AppUsers.GetCurrent().View.IsAdmin.ToBool();
                }

                if (hasAccess && isDevelopmentMode)
                {
                    if (this.AccessLevel.Equals(AccessLevel.LocalhostAdmin) && !isLocalHost)
                    {
                        hasAccess = false;
                    }
                }
            }

            if (!hasAccess)
            {
                Log.Information("Access to {Control} is denied to {User} from {IP}.", this,
                    userName, ipAddress);

                this.Page.Server.Transfer("~/Site/AccessIsDenied.aspx");
            }
        }
    }
}