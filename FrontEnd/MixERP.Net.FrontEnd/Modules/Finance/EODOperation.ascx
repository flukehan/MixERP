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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EODOperation.ascx.cs" Inherits="MixERP.Net.Core.Modules.Finance.EODOperation" %>

<div>
    <h1>End of Day Operation
    <asp:Literal runat="server" ID="ValueDateLiteral"></asp:Literal></h1>
    <div class="ui divider"></div>

    <div class="ui buttons">
        <button
            id="InitializeButton"
            runat="server"
            class="disabled"
            onclick="return(false);"
            data-popup=".initialize">
            <i class="icon alarm"></i>
            Initialize Day End
        </button>
        <button
            id="PerformEODButton"
            runat="server"
            class="disabled"
            onclick="return(false);"
            data-popup=".eod">
            <i class="icon wizard"></i>
            Perform EOD Operation
        </button>
    </div>

    <div class="ui large popup initialize">
        <div class="ui blue header">
            <i class="icon alarm"></i>
            <div class="content">
                About Initializing Day End
            </div>
        </div>
        <div class="ui divider"></div>
        <div class="content">
            <p>
                When you initialize day-end operation, the already logged-in application users
            including you are logged off on 120 seconds.
            </p>
            <p>
                During the day-end period, only users having elevated privilege are allowed to log-in.
            </p>
            <h4 class="ui horizontal red header divider">
                <i class="warning sign icon"></i>
                Warning
            </h4>
            <p class="error-message">
                Please do not close this window or navigate away from this page during initialization.
            </p>

            <button class="ui blue loading disabled button" onclick="return false" id="StartButton">Start</button>
        </div>
    </div>

    <div class="ui large popup eod">
        <div class="ui red header">
            <i class="icon alarm"></i>
            <div class="content">
                Performing EOD Operation
            </div>
        </div>
        <div class="ui divider"></div>
        <div class="content">
            <p>
                When you perform EOD operation for a particular date, no transaction on that date or before can be
            altered, changed, or deleted.
            </p>
            <p>
                During EOD operation, routine tasks such as interest calculation, settlements, and report generation are performed.
            </p>
            <p>
                This process is irreversible.
            </p>

            <button type="button" id="OKButton" class="ui small red loading disabled button" onclick="return (false);">OK</button>
        </div>
    </div>

    <div class="ui teal progress">
        <div class="bar">
            <div class="progress"></div>
        </div>
    </div>

    <h2 class="ui blue header initially hidden">EOD Console</h2>
    <div class="ui celled list">
    </div>
</div>
<script type="text/javascript">
    var counter = 120;
    var interval;
    var url;
    var data;
    var start;
    var receivingData = true;

    var triggers = $("[data-popup]");
    triggers.each(function () {
        $(this).popup({
            popup: $(this).data("popup"),
            hoverable: true,
            position: 'bottom left',
            delay: {
                show: 300,
                hide: 800
            }
        });

    });

    $(function () {
        //EOD Notices from PostgreSQL Server
        $.connection.dayOperationHub.client.getNotification = function (msg) {
            AddItem(msg);

            if (msg === "OK") {
                receivingData = false;
                notifyServer();
            };
        };
    });

    //Termination Signal
    function terminateListener(counter) {
        $("#PerformEODButton").addClass("disabled");
        $("#StartButton").addClass("disabled");
        $("#InitializeButton").addClass("disabled");

        $('.progress').progress({
            percent: counter
        });
    };

    //Connection to EOD SignalR Hub was successful.
    function connectionListener() {
        $("#StartButton").removeClass("disabled loading");
        $("#OKButton").removeClass("disabled loading");

        $("#StartButton").click(function () {
            $(this).addClass("disabled");
            var ajaxInitializeEODOperation = intializeEODOperation();

            ajaxInitializeEODOperation.success(function (msg) {
                if (msg.d.toString() === "true") {
                    triggers.popup('hide');

                    start = new Date;

                    interval = setInterval(sendMessage, 5000);
                    return;
                };

                alert('Access is denied');
            });

            ajaxInitializeEODOperation.fail(function (xhr) {
                logAjaxErrorMessage(xhr);
            });
        });

        $("#OKButton").click(function () {
            $(this).addClass("disabled");
            triggers.popup('hide');
            $(".ui.blue.header").removeClass("initially hidden");
            ShowProgress(0);
            $.connection.dayOperationHub.server.performEOD();
        });
    };

    var toggle = false;

    function AddItem(msg) {
        var timestamp = new Date().toISOString();

        var html = "<div class='item'><i class='ui big blue loading settings icon'></i><div class='content'><div class='ui blue header'>{0}</div>{1}</div></div>";

        html = String.format(html, timestamp, msg);

        $(".ui.celled.list").append(html);
    };

    function ShowProgress(counter) {
        if (receivingData) {
            if (counter > 100 || counter <= 0) {
                if (toggle === true) {
                    toggle = false;
                } else {
                    toggle = true;
                }
            };

            if (toggle) {
                counter += 5;
            } else {
                counter -= 5;
            };

            $('.progress').progress({
                percent: counter,
                limitValues: true,
                label: "ratio"
            });

            setTimeout(ShowProgress, 100, counter);

        } else {
            $('.progress').progress({
                percent: 100,
                limitValues: true,
                label: "ratio"
            });
        };
    };

    function notifyServer() {
        var ajaxStartNewDay = startNewDay();

        ajaxStartNewDay.success(function () {
            //Okay
        });

        ajaxStartNewDay.fail(function (xhr) {
            logAjaxErrorMessage(xhr);
        });

    };

    function startNewDay() {
        url = "/Modules/Finance/Services/EODOperation.asmx/StartNewDay";

        return getAjax(url, null);
    };

    function intializeEODOperation() {
        url = "/Modules/Finance/Services/EODOperation.asmx/InitializeEODOperation";

        return getAjax(url, null);
    };

    var sendMessage = function () {
        var elapsed = parseInt((new Date - start) / 1000);
        var progress = parseInt((elapsed / counter) * 100);

        if (progress > 100) {
            clearInterval(interval);
            return;
        };

        $.connection.baseHub.server.terminate(progress);
    };
</script>