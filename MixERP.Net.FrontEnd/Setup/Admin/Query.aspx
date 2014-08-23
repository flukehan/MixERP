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
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/MenuMaster.Master" AutoEventWireup="true"
    CodeBehind="Query.aspx.cs" Inherits="MixERP.Net.FrontEnd.Setup.Admin.Query" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="MixERP.Net.DBFactory" %>
<%@ Import Namespace="Npgsql" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceholder" runat="server">
    <script src="/bundles/scripts/query.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceholder" runat="server">
    <link href="/bundles/stylesheets/query.min.css" rel="stylesheet" />

    <style type="text/css">
        .CodeMirror {
            border: 1px solid #eee;
            height: auto;
        }

        .CodeMirror-scroll {
            overflow-y: hidden;
            overflow-x: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <div id="buttons">

        <asp:Button ID="ExecuteButton" runat="server" Text="<%$Resources:Titles, Execute %>" OnClick="ExecuteButton_Click" />
        <asp:Button ID="LoadButton" runat="server" Text="<%$Resources:Titles, Load %>" OnClick="LoadButton_Click" />
        <asp:Button ID="ClearButton" runat="server" Text="<%$Resources:Titles, Clear %>" OnClick="ClearButton_Click" />
        <asp:Button ID="SaveButton" runat="server" Text="<%$Resources:Titles, Save %>" OnClientClick="$('#QueryHidden').val(editor.getValue());" OnClick="SaveButton_Click" />
        <asp:Button ID="LoadBlankDBButton" runat="server" Text="Load Blank DB" OnClick="LoadBlankDBButton_Click" />
        <asp:Button ID="LoadSampleData" runat="server" Text="Load Sample Data" OnClick="LoadSampleData_Click" />

        <asp:Button ID="GoToTopButton" runat="server" Text="<%$Resources:Titles, GoToTop %>" OnClientClick="$('html, body').animate({ scrollTop: 0 }, 'slow');return(false);" />
    </div>

    <br />
    <br />
    <br />

    <asp:TextBox ID="QueryTextBox" runat="server" TextMode="MultiLine" Width="1000">
    </asp:TextBox>
    <asp:HiddenField ID="QueryHidden" runat="server" />

    <br />

    <div class="vpad16">
        <div style="width: 100%; max-height: 400px; overflow: auto">
            <asp:GridView ID="SQLGridView" EnableTheming="false" CssClass="grid2" HeaderStyle-CssClass="grid2-header" RowStyle-CssClass="grid2-row" AlternatingRowStyle-CssClass="grid2-row-alt" runat="server" ShowHeaderWhenEmpty="true">
            </asp:GridView>
            <asp:Literal ID="MessageLiteral" runat="server" />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceholder"
    runat="server">

    <script type="text/javascript">
        var mime = 'text/x-plsql';

        if (window.location.href.indexOf('mime=') > -1) {
            mime = window.location.href.substr(window.location.href.indexOf('mime=') + 5);
        }

        editor = CodeMirror.fromTextArea(document.getElementById('QueryTextBox'), {
            mode: mime,
            lineNumbers: true,
            matchBrackets: true,
            autoMatchParens: true,
            tabMode: 'spaces',
            tabSize: 4,
            indentUnit: 4,
            viewportMargin: Infinity,

        });

        editor.setOption("theme", "visual-studio");
        editor.refresh();

        $().ready(function () {
            var $scrollingDiv = $("#buttons");

            $(window).scroll(function () {
                $scrollingDiv
                    .stop()
                    .animate({ "marginTop": ($(window).scrollTop()) }, "slow");
            });
        });
    </script>
</asp:Content>

<script runat="server">
    protected void ClearButton_Click(object sender, EventArgs e)
    {
        this.QueryTextBox.Text = "";
    }
    protected void LoadButton_Click(object sender, EventArgs e)
    {
        this.LoadSql();
    }

    private void LoadSql()
    {
        string sql = File.ReadAllText(this.Server.MapPath("~/bundles/sql/mixerp-blank-db.sql"));
        this.QueryTextBox.Text = sql;
    }

    protected void LoadBlankDBButton_Click(object sender, EventArgs e)
    {
        string sql = File.ReadAllText(this.Server.MapPath("~/bundles/sql/mixerp-blank-db.sql"));
        using (DataTable table = DbOperations.GetDataTable(new NpgsqlCommand(sql)))
        {
            this.MessageLiteral.Text = string.Format("<div class='success'>{0} row(s) affected.</div>", table.Rows.Count);
            this.SQLGridView.DataSource = table;
            this.SQLGridView.DataBind();
        }
    }

    protected void LoadSampleData_Click(object sender, EventArgs e)
    {
        string sql = File.ReadAllText(this.Server.MapPath("~/bundles/sql/mixerp-db-sample.sql"));
        using (DataTable table = DbOperations.GetDataTable(new NpgsqlCommand(sql)))
        {
            this.MessageLiteral.Text = string.Format("<div class='success'>{0} row(s) affected.</div>", table.Rows.Count);
            this.SQLGridView.DataSource = table;
            this.SQLGridView.DataBind();
        }
    }

    protected void ExecuteButton_Click(object sender, EventArgs e)
    {
        try
        {
            using (DataTable table = DbOperations.GetDataTable(new NpgsqlCommand(this.QueryTextBox.Text)))
            {
                this.MessageLiteral.Text = string.Format("<div class='success'>{0} row(s) affected.</div>", table.Rows.Count);
                this.SQLGridView.DataSource = table;
                this.SQLGridView.DataBind();
            }
        }
        catch (Exception ex)
        {
            this.MessageLiteral.Text = @"<div class='error'>" + ex.Message + @"</div>";
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        string sql = this.QueryHidden.Value;

        if (!string.IsNullOrWhiteSpace(sql))
        {
            string path = this.Server.MapPath("~/db/en-US/mixerp.postgresql.bak.sql");
            File.Delete(path);
            File.WriteAllText(path, sql, Encoding.UTF8);
        }
    }
</script>
