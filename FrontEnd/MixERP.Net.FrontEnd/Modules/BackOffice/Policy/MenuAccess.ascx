<%--
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
along with MixERP.  If not, see <http://www.gnu.org/licenses />.
--%>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuAccess.ascx.cs" Inherits="MixERP.Net.Core.Modules.BackOffice.Policy.MenuAccess" %>
<%@ Import Namespace="MixERP.Net.WebControls.Common" %>
<%@ Import Namespace="MixERP.Net.Common.Helpers" %>
<%@ Import Namespace="MixERP.Net.Common" %>

<asp:PlaceHolder runat="server" ID="Placeholder1"></asp:PlaceHolder>

<script type="text/javascript">
    $(function() {
        var grid = $("#MenuAccessGridView");
        var checkAllButton = $("#CheckAllButton");
        var uncheckAllButton = $("#UncheckAllButton");


        grid.find("input").removeAttr("disabled");

        checkAllButton.click(function() {
            grid.find("input").prop("checked", true);
        });

        uncheckAllButton.click(function() {
            grid.find("input").prop("checked", false);
        });

        grid.find("tr").click(function() {
            var el = $(this).find("input");
            el.prop("checked", !el.prop("checked"));
        });
    });

    function updateSelection() {
        var selectedMenusHidden = $("#SelectedMenusHidden");
        var selectedElements = $("input:checked");
        var items = [];

        selectedElements.each(function() {
            var menuId = $(this).closest("tr").find("td:nth-child(3)").html();
            items.push(menuId);
        });

        selectedMenusHidden.val(items.join(","));
    };

</script>
<script runat="server">

    public override void OnControlLoad(object sender, EventArgs e)
    {
        this.CreateFormPanel(this.Placeholder1);
        this.CreateGridPanel(this.Placeholder1);
        this.CreateHiddenField(this.Placeholder1);
        this.BindGrid();
    }

    private void CreateHiddenField(Control container)
    {
        this.selectedMenusHidden = new HiddenField();
        this.selectedMenusHidden.ID = "SelectedMenusHidden";
        container.Controls.Add(this.selectedMenusHidden);
    }

    private void CreateHeader(Control container)
    {
        using (HtmlGenericControl header = HtmlControlHelper.GetPageHeader("Menu Access Policy"))
        {
            container.Controls.Add(header);
        }
    }

    private void CreateFormPanel(Control container)
    {
        using (HtmlGenericControl formSegment = HtmlControlHelper.GetFormSegment("ui tiny pink form segment"))
        {
            formSegment.Attributes.Add("style", "position:fixed;right:4px;top:40px;");
            this.CreateHeader(formSegment);

            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel("Select User", "UserSelect"))
                {
                    field.Controls.Add(label);
                }

                this.userSelect = new DropDownList();
                userSelect.ID = "UserSelect";
                field.Controls.Add(userSelect);
                this.userSelect.DataSource = MixERP.Net.Core.Modules.BackOffice.Data.Admin.User.GetUsers();
                this.userSelect.DataTextField = "UserName";
                this.userSelect.DataValueField = "UserId";
                this.userSelect.DataBind();

                formSegment.Controls.Add(field);
            }

            this.CreateButtons(formSegment);

            container.Controls.Add(formSegment);
        }
    }

    private void CreateButtons(Control container)
    {
        using (HtmlGenericControl buttons = new HtmlGenericControl("div"))
        {
            buttons.Attributes.Add("class", "ui buttons vpad8");

            this.showButton = new Button();
            this.showButton.CssClass = "ui red button";
            this.showButton.Text = "Show";
            this.showButton.Click += this.ShowButton_Click;
            buttons.Controls.Add(this.showButton);

            using (HtmlInputButton checkAllButton = new HtmlInputButton())
            {
                checkAllButton.ID = "CheckAllButton";
                checkAllButton.Attributes.Add("class", "ui blue button");
                checkAllButton.Value = "Check All";

                buttons.Controls.Add(checkAllButton);
            }

            using (HtmlInputButton uncheckAllButton = new HtmlInputButton())
            {
                uncheckAllButton.ID = "UncheckAllButton";
                uncheckAllButton.Attributes.Add("class", "ui pink button");
                uncheckAllButton.Value = "Uncheck All";

                buttons.Controls.Add(uncheckAllButton);
            }

            this.saveButton = new Button();
            this.saveButton.CssClass = "ui green button";
            this.saveButton.Text = "Save";
            this.saveButton.Click += this.SaveButton_Click;
            this.saveButton.OnClientClick = "return updateSelection();";
            buttons.Controls.Add(this.saveButton);

            container.Controls.Add(buttons);
        }
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        int userId = Conversion.TryCastInteger(this.userSelect.SelectedValue);
        int officeId = CurrentSession.GetOfficeId();
        string menus = this.selectedMenusHidden.Value;

        if (userId.Equals(0) || officeId.Equals(0))
        {
            return;
        }

        MixERP.Net.Core.Modules.BackOffice.Data.Policy.Menu.SaveMenuPolicy(userId, officeId, menus);
        this.BindGrid();
    }

    private void ShowButton_Click(object sender, EventArgs e)
    {
        this.BindGrid();
    }

    private void CreateGridPanel(Control container)
    {
        this.grid = new MixERPGridView();
        grid.ID = "MenuAccessGridView";

        container.Controls.Add(grid);
    }

    private void BindGrid()
    {
        int userId = Conversion.TryCastInteger(this.userSelect.SelectedValue);
        int officeId = CurrentSession.GetOfficeId();
        string culture = CurrentSession.GetCulture().Name;

        if (userId.Equals(0) || officeId.Equals(0))
        {
            return;
        }

        grid.DataSource = MixERP.Net.Core.Modules.BackOffice.Data.Policy.Menu.GetMenuPolicy(userId, officeId, culture);
        grid.DataBind();
    }

    #region IDisposable

    private DropDownList userSelect;
    private MixERPGridView grid;
    private Button showButton; //ShowButton_Click
    private Button saveButton; //SaveButton_Click
    private HiddenField selectedMenusHidden;

    #endregion</script>