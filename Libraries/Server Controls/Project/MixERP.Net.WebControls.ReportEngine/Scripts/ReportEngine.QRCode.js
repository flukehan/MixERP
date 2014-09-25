$(document).ready(function () {
    var reportEngineQRCode = $(".reportEngineQRCode");

    reportEngineQRCode.each(function () {
        var element = $(this);

        var typeNumber = element.attr("data-qrcodetypenumber");
        var height = parseInt(element.attr("data-qrcodeheight"));
        var width = parseInt(element.attr("data-qrcodewidth"));
        var foregroundColor = element.attr("data-qrcodeforegroundcolor");
        var backgroundColor = element.attr("data-qrcodebackgroundcolor");
        var render = element.attr("data-qrcoderender");
        var value = element.attr("data-qrcodevalue");

        element.qrcode(
        {
            render: render,
            text: value,
            width: width,
            height: height,
            typeNumber: typeNumber,
            background: backgroundColor,
            foreground: foregroundColor
        });
    });
});