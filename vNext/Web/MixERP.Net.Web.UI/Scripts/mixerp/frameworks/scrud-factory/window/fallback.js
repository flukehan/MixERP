function windowFallback() {
    if (typeof window.scrudAreYouSureLocalized === "undefined") {
        window.scrudAreYouSureLocalized = "Do you really want to do this?";
    };

    if (typeof window.scrudNothingSelectedLocalized === "undefined") {
        window.scrudNothingSelectedLocalized = "Nothing was selected!";
    };

    if (typeof window.requiredLocalized === "undefined") {
        window.requiredLocalized = "This field should not be empty.";
    };

    if (typeof window.invalidNumberLocalized === "undefined") {
        window.invalidNumberLocalized = "This does not look like a number.";
    };

    if (typeof window.decimalSeparator === "undefined") {
        window.decimalSeparator = ".";
    };

    if (typeof window.thousandSeparator === "undefined") {
        window.thousandSeparator = ",";
    };
};