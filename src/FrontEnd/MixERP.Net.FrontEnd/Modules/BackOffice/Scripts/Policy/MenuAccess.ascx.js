$(function () {
    var grid = $("#MenuAccessGridView");
    var checkAllButton = $("#CheckAllButton");
    var uncheckAllButton = $("#UncheckAllButton");

    grid.find("input").removeAttr("disabled");

    checkAllButton.click(function () {
        grid.find("input").prop("checked", true);
    });

    uncheckAllButton.click(function () {
        grid.find("input").prop("checked", false);
    });

    grid.find("tr").click(function () {
        var el = $(this).find("input");
        el.prop("checked", !el.prop("checked"));
    });
});

function updateSelection() {
    var selectedMenusHidden = $("#SelectedMenusHidden");
    var selectedElements = $("input:checked");
    var items = [];

    selectedElements.each(function () {
        var menuId = $(this).closest("tr").find("td:nth-child(3)").html();
        items.push(menuId);
    });

    selectedMenusHidden.val(items.join(","));
};