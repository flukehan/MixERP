//Semantic UI Tab Support
$(document).ready(function () {
    var tabItems = $('.tabular .item');

    if (tabItems && tabItems.length > 0) {
        tabItems.tab();
    };

    //Semantic UI Button Support
    var buttons = $(".buttons .button");

    buttons.on("click", function () {
        buttons.removeClass("active");
        $(this).addClass("active");
    });
});