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

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatabaseBackup.ascx.cs" Inherits="MixERP.Net.Core.Modules.BackOffice.Admin.DatabaseBackup" %>

<h1>Backup Database</h1>

<div class="ui form segment">
    <div class="inline fields">
        <div class="field">
            <label for="label">
                Enter Backup Name
            </label>
            <input type="text" id="BackupNameInputText" runat="server" />
        </div>
        <input type="button" class="ui red button disabled loading" value="Backup Now" id="BackupButton" />
        <input type="button" class="ui green button" value="View Backups" />
    </div>


    <div class="ui teal progress">
        <div class="bar">
            <div class="progress"></div>
        </div>
    </div>

    <h2 class="ui blue header initially hidden">Backup Console</h2>
    <div id="Messages" style="overflow: auto;">
        <div class="ui celled list">
        </div>
    </div>
</div>

<script type="text/javascript">
    var list = $(".ui.celled.list");
    var messages = $("#Messages");
    var backupButton = $("#BackupButton");
    var counter = 0;
    var header = $(".ui.blue.header");
    var backupNameInputText = $("#BackupNameInputText");


    function AddItem(msg, cls) {
        if (!cls) {
            cls = "blue";
        };

        var html = "<div class='item'><i class='ui {0} loading recycle icon'></i><div class='content'>{1}. {2} : {3}</div></div>";
        counter++;

        var timestamp = new Date().toISOString();
        html = String.format(html, cls, counter, timestamp, msg);

        list.append(html);
        messages.scrollTop(messages.prop("scrollHeight"));
    };

    function InitializeMessage()
    {
        list.html("");
        counter = 0;
        messages.css("height", $(document).height() - 350 + "px").addClass("ui segment");
    }


    //Connection to EOD SignalR Hub was successful.
    function connectionListener() {
        backupButton.removeClass("disabled loading");

        backupButton.click(function () {
            var backupName = backupNameInputText.val();

            if (!isValidFileName(backupName)) {
                makeDirty(backupNameInputText);
                return;
            };

            removeDirty(backupNameInputText);

            $(this).addClass("disabled loading");
            header.removeClass("initially hidden");
            InitializeMessage();
            $.connection.dbHub.server.backupDatabase(backupName);
        });
    };

    function isValidFileName(fileName) {
        if (isNullOrWhiteSpace(fileName)) {
            return false;
        };

        return true;
    };

    $(function () {
        $.connection.dbHub.client.getNotification = function (msg) {
            AddItem(msg);
        };

        $.connection.dbHub.client.backupCompleted = function (msg) {
            backupButton.removeClass("disabled loading");
            AddItem(msg);
        };

        $.connection.dbHub.client.backupFailed = function (msg) {
            backupButton.removeClass("disabled loading");
            AddItem(msg, "red");
        };
    });


</script>
