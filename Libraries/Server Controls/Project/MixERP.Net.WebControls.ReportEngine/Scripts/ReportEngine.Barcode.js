$(document).ready(function () {
    var reportEngineBarCode = $(".reportEngineBarCode");

    reportEngineBarCode.each(function () {
        var element = $(this);
        var borderLineColor = element.attr("data-barcodelinecolor");
        var borderBackgroundColor = element.attr("data-barcodebackgroundcolor");
        var barCodeTextAlign = element.attr("data-barcodetextalign");
        var barcodeFont = element.attr("data-barcodefont");
        var barcodeQuite = parseInt(element.attr("data-barcodequite"));
        var barcodeHeight = parseInt(element.attr("data-barcodeheight"));
        var barcodeWidth = parseInt(element.attr("data-barcodewidth"));
        var barcodeFontSize = parseInt(element.attr("data-barcodefontsize"));
        var barcodeDisplayValue = (element.attr("data-barcodedisplayvalue").toLowerCase() === "true");
        var barcodeFormat = element.attr("data-barcodeformat");
        var barcodeValue = element.attr("value");

        element.JsBarcode(barcodeValue,
        {
            lineColor: borderLineColor,
            backgroundColor: borderBackgroundColor,
            textAlign: barCodeTextAlign,
            font: barcodeFont,
            quite: barcodeQuite,
            height: barcodeHeight,
            width: barcodeWidth,
            fontSize: barcodeFontSize,
            displayValue: barcodeDisplayValue,
            format: barcodeFormat
        });
    });
});