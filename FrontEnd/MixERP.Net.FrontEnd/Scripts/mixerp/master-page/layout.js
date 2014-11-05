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

    if (mainContent) {
        mainContent.css("width", mainContent.width() + "px");
    };
};