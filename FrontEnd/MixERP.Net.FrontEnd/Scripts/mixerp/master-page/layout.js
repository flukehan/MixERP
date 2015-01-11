(function () {
    fixLayout();
})();

$(window).on('resize', function () {
    fixLayout();
});

function fixLayout() {
    if (contentRow) {
        var contentHeight = contentRow.height();
        var docHeight = $(document).height();
        var footerHeight = footer.height();
        var margin = 0;

        if (contentHeight < docHeight - footerHeight - margin) {
            contentRow.css("height", docHeight - footerHeight - margin + "px");
        };
    };

    if (topMenu) {
        if (mainContent) {
            mainContent.css("width", topMenu.width() + "px");
        };

        if (fullWidthContainer) {
            fullWidthContainer.css("width", topMenu.width() + "px");
        };
    };
};