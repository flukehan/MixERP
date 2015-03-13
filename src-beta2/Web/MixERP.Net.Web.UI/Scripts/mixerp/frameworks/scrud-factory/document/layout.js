function scrudLayout() {
    var gridPanel = $("#GridPanel");
    var scrudUpdatePanel = $("#ScrudUpdatePanel");

    if (gridPanel && scrudUpdatePanel) {
        gridPanel.css("width", scrudUpdatePanel.width() + "px");
    };
};