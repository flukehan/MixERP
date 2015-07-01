function popUnder(div, button) {
    div.removeClass("initially hidden");
    div.show(500);

    div.position({
        my: "left top",
        at: "left bottom",
        of: button
    });
};