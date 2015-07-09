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

var el = document.getElementById('map-container');
var colors = ['#A8F792', '#F2C3FA', '#E892F7', '#D4C3FA', '#C3C4FA', '#DB9E58', '#58DB91', '#F792B4', '#DBCC58', '#DB9E58', '#58DBDB', '#D558DB', '#D6313C', '#3134D6', '#A1CCA2'];
var table = $("#SalesByGeographyGridView");

function parseTable() {
    var items = [];
    var data = {};
    var fills = {};

    var counter = 0;
    table.find("tbody tr").each(function () {
        counter++;
        var countryCode = $(this).find("td:first-child").html();
        var sales = parseFloat2($(this).find("td:last-child").html());

        var code = iso3Countries[countryCode];

        data[code] = {
            fillKey: code,
            isoCode: countryCode,
            sales: getFormattedNumber(sales)
        };

        fills[code] = colors[counter];
    });

    fills['defaultFill'] = '#DCE3E8';

    items.push(data);
    items.push(fills);

    table.remove();

    return items;
};

function createMap() {
    var items = parseTable();

    var map = new Datamap({
        element: document.getElementById('map-container'),
        fills: items[1],
        dataType: 'json',
        data: items[0],
        geographyConfig: {
            popupTemplate: function (geo, data) {
                if (data) {
                return ['<div class="ui compact segment">' +
                    '<div class="ui small header"><i class="' + data.isoCode.toLowerCase() + ' flag"></i><div class="content">'
                    + geo.properties.name + '</div>',
                    '<div class="sub header">' + Resources.Titles.TotalSales() + " " + baseCurrencyCode + data.sales,
                    '</div></div>'].join('');
                }
            }
        }
    });
};

$(document).ready(function () {
    createMap();
});


