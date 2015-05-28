function popUnder(div, button) {
    div.css("position", "fixed");

    div.position({
        my: "left top",
        at: "left bottom",
        of: button,
        collision: "fit"
    });

    div.show(500);
};