var validateByControlId = function (controlId) {
    if (typeof Page_ClientValidate === "function") {
        Page_ClientValidate(controlId);
    };
};