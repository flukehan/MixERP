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
using MixERP.Net.Common.Extensions;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Finance.Data;
using MixERP.Net.Entities.Contracts;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.i18n.Resources;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.Finance
{
    public partial class EODOperation : MixERPUserControl, ITransaction
    {
        private int officeId;
        private EODStatus status;

        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.InitializeEODStatus();

            this.CreateHeader(this.Placeholder1);
            this.CreateDivider(this.Placeholder1);
            this.CreateButtons(this.Placeholder1);
            this.CreateAboutInitializing(this.Placeholder1);
            this.CreatePerformingEOD(this.Placeholder1);
            this.CreateProgress(this.Placeholder1);
            this.CreateEODConsole(this.Placeholder1);
            this.CreateList(this.Placeholder1);
        }

        private void InitializeEODStatus()
        {
            this.officeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
            status = Data.EODOperation.GetStatus(AppUsers.GetCurrentUserDB(), officeId);
        }

        private void CreateHeader(Control container)
        {
            using (HtmlGenericControl h1 = new HtmlGenericControl("h1"))
            {
                h1.InnerText = Titles.EndOfDayOperation + " (" + this.GetValueDateString() + ")";
                container.Controls.Add(h1);
            }
        }

        private string GetValueDateString()
        {
            if (status != null)
            {
                return status.ValueDate.ToString("d");
            }

            return string.Empty;
        }

        private void CreateDivider(Control container)
        {
            using (HtmlGenericControl divider = HtmlControlHelper.GetDivider())
            {
                container.Controls.Add(divider);
            }
        }

        private void CreateProgress(Control container)
        {
            using (HtmlGenericControl progressBar = new HtmlGenericControl("div"))
            {
                progressBar.Attributes.Add("class", "ui teal progress");

                using (HtmlGenericControl bar = new HtmlGenericControl("div"))
                {
                    bar.Attributes.Add("class", "bar");

                    using (HtmlGenericControl progress = new HtmlGenericControl("div"))
                    {
                        progress.Attributes.Add("class", "progress");
                        bar.Controls.Add(progress);
                    }

                    progressBar.Controls.Add(bar);
                }

                container.Controls.Add(progressBar);
            }
        }

        private void CreateEODConsole(Control container)
        {
            using (HtmlGenericControl console = new HtmlGenericControl("h2"))
            {
                console.InnerText = Titles.EODConsole;
                console.Attributes.Add("class", "ui blue header initially hidden");
                container.Controls.Add(console);
            }
        }

        private void CreateList(Control container)
        {
            using (HtmlGenericControl list = new HtmlGenericControl("div"))
            {
                list.Attributes.Add("class", "ui celled list");
                container.Controls.Add(list);
            }
        }

        #region Buttons

        private void CreateButtons(Control container)
        {
            using (HtmlGenericControl buttons = new HtmlGenericControl("div"))
            {
                this.CreateInitializeButton(buttons);
                this.CreatePerformEODButton(buttons);

                container.Controls.Add(buttons);
            }
        }

        private void CreateInitializeButton(HtmlGenericControl container)
        {
            using (HtmlButton initializeButton = new HtmlButton())
            {
                initializeButton.ID = "InitializeButton";

                if (this.status.IsInitialized)
                {
                    initializeButton.Attributes.Add("class", "ui blue disabled button");
                }
                else
                {
                    initializeButton.Attributes.Add("class", "ui blue button");
                }

                initializeButton.Attributes.Add("onclick", "return false;");
                initializeButton.Attributes.Add("data-popup", ".initialize");


                using (HtmlGenericControl i = new HtmlGenericControl("i"))
                {
                    i.Attributes.Add("class", "icon alarm");
                    initializeButton.Controls.Add(i);
                }

                using (Literal buttonText = new Literal())
                {
                    buttonText.Text = Titles.InitializeDayEnd;
                    initializeButton.Controls.Add(buttonText);
                }


                container.Controls.Add(initializeButton);
            }
        }

        private void CreatePerformEODButton(HtmlGenericControl container)
        {
            using (HtmlButton performEODButton = new HtmlButton())
            {
                performEODButton.ID = "PerformEODButton";

                if (this.status.IsInitialized)
                {
                    performEODButton.Attributes.Add("class", "ui red button");
                }
                else
                {
                    performEODButton.Attributes.Add("class", "ui red disabled button");
                }


                performEODButton.Attributes.Add("onclick", "return false;");
                performEODButton.Attributes.Add("data-popup", ".eod");


                using (HtmlGenericControl i = new HtmlGenericControl("i"))
                {
                    i.Attributes.Add("class", "icon wizard");
                    performEODButton.Controls.Add(i);
                }

                using (Literal buttonText = new Literal())
                {
                    buttonText.Text = Titles.PerformEODOperation;
                    performEODButton.Controls.Add(buttonText);
                }


                container.Controls.Add(performEODButton);
            }
        }

        #endregion

        #region About Initializing

        private void CreateAboutInitializing(Control container)
        {
            using (HtmlGenericControl popup = new HtmlGenericControl("div"))
            {
                popup.Attributes.Add("class", "ui large popup initialize");

                this.CreateAboutInitializingHeader(popup);
                this.CreateAboutInitializingDivider(popup);
                this.CreateAboutInitializingContent(popup);

                container.Controls.Add(popup);
            }
        }

        private void CreateAboutInitializingHeader(HtmlGenericControl container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("div"))
            {
                header.Attributes.Add("class", "ui blue header");

                using (HtmlGenericControl i = new HtmlGenericControl())
                {
                    i.Attributes.Add("class", "icon alarm");
                    header.Controls.Add(i);
                }

                using (HtmlGenericControl content = new HtmlGenericControl())
                {
                    content.Attributes.Add("class", "content");
                    content.InnerText = Titles.AboutInitializingDayEnd;

                    header.Controls.Add(content);
                }


                container.Controls.Add(header);
            }
        }

        private void CreateAboutInitializingDivider(HtmlGenericControl container)
        {
            using (HtmlGenericControl divider = HtmlControlHelper.GetDivider())
            {
                container.Controls.Add(divider);
            }
        }

        private void CreateAboutInitializingContent(HtmlGenericControl container)
        {
            using (HtmlGenericControl content = new HtmlGenericControl("div"))
            {
                using (HtmlGenericControl p = new HtmlGenericControl("p"))
                {
                    p.InnerText = Messages.EODLogsOffUsers;
                    content.Controls.Add(p);
                }

                using (HtmlGenericControl p = new HtmlGenericControl("p"))
                {
                    p.InnerText = Messages.EODElevatedPriviledgeCanLogIn;
                    content.Controls.Add(p);
                }

                using (HtmlGenericControl h4 = new HtmlGenericControl("p"))
                {
                    h4.Attributes.Add("class", "ui horizontal red header divider");

                    using (HtmlGenericControl i = new HtmlGenericControl("i"))
                    {
                        i.Attributes.Add("class", "warning sign icon");
                        h4.Controls.Add(i);
                    }

                    using (Literal h4Text = new Literal())
                    {
                        h4Text.Text = Titles.Warning;
                        h4.Controls.Add(h4Text);
                    }

                    content.Controls.Add(h4);
                }

                using (HtmlGenericControl p = new HtmlGenericControl("p"))
                {
                    p.InnerText = Messages.EODDoNotCloseWindow;
                    content.Controls.Add(p);
                }


                this.CreateStartButton(content);

                container.Controls.Add(content);
            }
        }

        private void CreateStartButton(HtmlGenericControl container)
        {
            using (HtmlButton startButton = new HtmlButton())
            {
                startButton.ID = "StartButton";
                startButton.Attributes.Add("class", "ui blue loading disabled button");
                startButton.Attributes.Add("onclick", "return false;");
                startButton.InnerText = Titles.Start;

                container.Controls.Add(startButton);
            }
        }

        #endregion

        #region Performing EOD

        private void CreatePerformingEOD(Control container)
        {
            using (HtmlGenericControl popup = new HtmlGenericControl("div"))
            {
                popup.Attributes.Add("class", "ui large popup eod");

                this.CreatePerformingEODHeader(popup);
                this.CreatePerformingEODDivider(popup);
                this.CreatePerformingEODContent(popup);

                container.Controls.Add(popup);
            }
        }

        private void CreatePerformingEODHeader(HtmlGenericControl container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("div"))
            {
                header.Attributes.Add("class", "ui red header");

                using (HtmlGenericControl i = new HtmlGenericControl("i"))
                {
                    i.Attributes.Add("class", "alarm icon");
                    header.Controls.Add(i);
                }


                using (HtmlGenericControl content = new HtmlGenericControl("div"))
                {
                    content.Attributes.Add("class", "content");
                    content.InnerText = Titles.PerformingEODOperation;

                    header.Controls.Add(content);
                }


                container.Controls.Add(header);
            }
        }

        private void CreatePerformingEODDivider(HtmlGenericControl container)
        {
            using (HtmlGenericControl divider = HtmlControlHelper.GetDivider())
            {
                container.Controls.Add(divider);
            }
        }

        private void CreatePerformingEODContent(HtmlGenericControl container)
        {
            using (HtmlGenericControl content = new HtmlGenericControl("div"))
            {
                using (HtmlGenericControl p = new HtmlGenericControl("p"))
                {
                    p.InnerText = Messages.EODTransactionPosting;
                    content.Controls.Add(p);
                }

                using (HtmlGenericControl p = new HtmlGenericControl("p"))
                {
                    p.InnerText = Messages.EODRoutineTasks;
                    content.Controls.Add(p);
                }

                using (HtmlGenericControl p = new HtmlGenericControl("p"))
                {
                    p.InnerText = Messages.EODProcessIsIrreversible;
                    content.Controls.Add(p);
                }

                this.CreateOKButton(content);
                container.Controls.Add(content);
            }
        }

        private void CreateOKButton(HtmlGenericControl container)
        {
            using (HtmlButton okButton = new HtmlButton())
            {
                okButton.ID = "OKButton";
                okButton.Attributes.Add("class", "ui small red loading disabled button");
                okButton.Attributes.Add("onclick", "return false;");
                okButton.InnerText = Titles.OK;

                container.Controls.Add(okButton);
            }
        }

        #endregion
    }
}