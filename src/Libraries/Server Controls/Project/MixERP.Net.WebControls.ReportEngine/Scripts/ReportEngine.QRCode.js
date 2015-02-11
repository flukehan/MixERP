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
    prepareQRCodes();
});

function prepareQRCodes() {
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
};