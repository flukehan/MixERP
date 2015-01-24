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

function InitializeMessage() {
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

