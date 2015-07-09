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

<asp:PlaceHolder runat="server" ID="Placeholder1"></asp:PlaceHolder>


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
        $.connection.dayOperationHub.client.getNotification = function (msg, condition) {
            AddItem(msg);

            if (condition.toLowerCase() === "error") {
                receivingData = false;

                $(".blue").removeClass("blue").addClass("red");
                $("h2").removeClass("blue").addClass("red");
                $(".loading").removeClass("loading");
                $(".progress").removeClass("success").addClass("error");
            };

            if (msg === "OK") {
                receivingData = false;
                $(".loading").removeClass("loading");
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