$(document).ready(function () {
    var searchInput = $("#SearchInput");

    shortcut.add("CTRL+M", function () {
        searchInput.focus();
    });

    shortcut.add("CTRL+,", function () {
        $("ul").find("[data-selected='true']").each(function () {
            $(this).find("a").focus();
        });
    });

    searchInput.focus();
});
