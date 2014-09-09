<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QueryTool.ascx.cs" Inherits="MixERP.Net.Core.Modules.BackOffice.Admin.QueryTool" %>

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

<div id="buttons">

    <asp:Button ID="ExecuteButton" runat="server" OnClick="ExecuteButton_Click" />
    <asp:Button ID="LoadButton" runat="server" OnClick="LoadButton_Click" />
    <asp:Button ID="ClearButton" runat="server" OnClick="ClearButton_Click" />
    <asp:Button ID="SaveButton" runat="server" OnClientClick="$('#QueryHidden').val(editor.getValue());" OnClick="SaveButton_Click" />
    <asp:Button ID="LoadBlankDBButton" runat="server" Text="Load Blank DB" OnClick="LoadBlankDBButton_Click" />
    <asp:Button ID="LoadSampleData" runat="server" Text="Load Sample Data" OnClick="LoadSampleData_Click" />

    <asp:Button ID="GoToTopButton" runat="server" OnClientClick="$('html, body').animate({ scrollTop: 0 }, 'slow');return(false);" />
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
        <asp:GridView ID="SQLGridView" EnableTheming="false" CssClass="table table-bordered" HeaderStyle-CssClass="grid2-header" RowStyle-CssClass="grid2-row" AlternatingRowStyle-CssClass="grid2-row-alt" runat="server" ShowHeaderWhenEmpty="true">
        </asp:GridView>
        <asp:Literal ID="MessageLiteral" runat="server" />
    </div>
</div>

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