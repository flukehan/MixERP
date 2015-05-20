function addShortcuts() {
    window.shortcut.add("ESC", function () {
        if ($('#' + formPanelId).is(':hidden')) {
            return;
        };

        if ($("#colorbox").css("display") === "block") {
            return;
        };

        if ($("body").attr("class") === "modal-open") {
            return;
        };

        var result = confirm(window.scrudAreYouSureLocalized);
        if (result) {
            $('#' + cancelButtonId).click();
        }
    });

    window.shortcut.add("RETURN", function () {
        scrudSelectAndClose();
    });

    window.shortcut.add("CTRL+SHIFT+C", function () {
        scrudShowCompact();
    });

    window.shortcut.add("CTRL+SHIFT+S", function () {
        scrudShowAll();
    });

    window.shortcut.add("CTRL+SHIFT+A", function () {
        return (scrudAddNew());
    });

    window.shortcut.add("CTRL+SHIFT+E", function () {
        $('#EditButtontop').click();
    });

    window.shortcut.add("CTRL+SHIFT+D", function () {
        $('#DeleteButtontop').click();
    });

    window.shortcut.add("CTRL+SHIFT+P", function () {
        scrudPrintGridView();
    });
};