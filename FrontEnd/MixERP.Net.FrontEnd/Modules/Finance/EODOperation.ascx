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

<h1>End of Day Operation (2013/04/06)</h1>
<div class="ui divider"></div>

<div class="ui buttons">
    <button class="ui blue button" id="InitializeButton" onclick="return(false);" data-popup=".initialize">
        <i class="icon alarm"></i>
        Initialize Day End
    </button>
    <button class="ui red disabled button" id="PerformEODButton" onclick="return(false);" data-popup=".eod">
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
            When you initialize day-end operation, the already logged-in application users including you are notified to save their work
            and log-off within 120 seconds.
        </p>
        <p>
            During the day-end period, only users having elevated privilege are allowed to log-in.
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
    </div>
</div>

<br />
<br />

<div class="ui teal progress">
    <div class="bar">
        <div class="progress"></div>
    </div>
    <div class="label">Sending Termination Signal</div>
</div>

<script type="text/javascript">
    var counter = 120;
    var start = new Date;
    var interval;

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

    function terminateListener(c) {
        $("#StartButton").addClass("disabled");
        $("#InitializeButton").addClass("disabled");

        $('.progress').progress({
            percent: c
        });

        if (c === 100) {
            $("#PerformEODButton").removeClass("disabled");
        };

    };

    function connectionListener() {
        $("#StartButton").removeClass("disabled").removeClass("loading");

        $("#StartButton").click(function () {
            triggers.popup('hide');
            interval = setInterval(sendMessage, 1000);

        });
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