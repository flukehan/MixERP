//Swallow the CTRL + P combination
jQuery(document).bind("keyup keydown", function (e) {
    if (e.ctrlKey && e.keyCode == 80) {
        e.preventDefault();
        return false;
    }

    return true;
});

$(document).ready(function () {
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
