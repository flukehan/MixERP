function setVisible(targetControl, visible, timeout) {
    if (visible) {
        targetControl.show(timeout);
        return;
    };

    targetControl.hide(timeout);
};

function addNotification(message, onclick) {
    var count = parseInt2($("#NotificationMenu span").addClass("ui red label").html());
    count++;
    $("#NotificationMenu span").addClass("ui red label").html(count);

    var item = $("<div />");
    item.attr("class", "item");

    if (onclick) {
        item.attr("onclick", onclick);
    };

    item.html(message);

    $("#Notification").append(item);
};