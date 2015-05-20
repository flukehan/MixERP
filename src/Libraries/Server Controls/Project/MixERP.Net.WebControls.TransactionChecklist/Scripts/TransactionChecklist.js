var cancelButton = $("#CancelButton");
var withdrawDiv = $("#WithdrawDiv");
var withdrawAnchor = $("#WithdrawAnchor");
var viewReportAnchor = $("#ViewReportAnchor");
var emailHidden = $("#EmailHidden");
var okButton = $("#OKButton");


$(document).ready(function () {
    if (withdrawAnchor.length) {
        if (withdrawDiv) {
            withdrawDiv.position({
                my: "left top",
                at: "right top",
                of: withdrawAnchor,
                collision: "flip"
            });

            withdrawDiv.hide();
        };

        withdrawAnchor.click(function () {
            withdrawDiv.toggle(200);
        });
    };

    var url = viewReportAnchor.attr("data-url");
    if (url) {
        prepareEmail(url);
    };
});

okButton.click(function() {
    emailHidden.val("");
});

cancelButton.click(function () {
    withdrawDiv.toggle(200);
});

function prepareEmail(url) {
    $.get(
        url,
        function (response) {
            emailHidden.val(response);
        });
};