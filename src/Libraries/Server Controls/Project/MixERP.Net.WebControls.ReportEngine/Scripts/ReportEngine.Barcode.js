/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/
$(document).ready(function () {
    prepareBarCodes();
});

function prepareBarCodes() {
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
};