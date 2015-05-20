///<reference path="../bundles/scripts/master-page.min.js"/>
window.currencySymbol = "Rs. ";
window.currencyDecimalPlaces = "2";

var itemTable = $("<table> " +
            "    <thead> " +
            "        <tr> " +
            "           <th>Item Name</th> " +
            "           <th>Qty</th> " +
            "           <th>Price</th> " +
            "	</tr> " +
            "    </thead> " +
            "    <tbody > " +
            "	<tr> " +
            "	    <td>Item 1</td> " +
            "	    <td>100</td> " +
            "	    <td>Rs. 4000.00</td> " +
            "	</tr> " +
            "	<tr> " +
            "	    <td>Item 2</td> " +
            "	    <td>2</td> " +
            "	    <td>Rs. 13000.00</td> " +
            "	</tr> " +
            "	<tr> " +
            "	    <td>Item 3</td> " +
            "	    <td>55</td> " +
            "	    <td>Rs. 3708.00</td> " +
            "	</tr> " +
            "	<tr> " +
            "	    <td>Item 4</td> " +
            "	    <td>102</td> " +
            "	    <td>Rs. 12490.00</td> " +
            "	</tr> " +
            "    </tbody> " +
            "</table> ");

QUnit.test("cell.js -> sumOfColumn", function (assert) {
    var expected = 259;
    var actual = parseFloat2(sumOfColumn(itemTable, 1));

    assert.equal(actual, expected, "The function returned expected value \"" + expected + "\".");

    expected = 33198;
    actual = parseFloat2(sumOfColumn(itemTable, 2));

    assert.equal(actual, expected, "The function returned expected \"" + expected + "\".");
});
QUnit.test("cell.js -> getColumnText", function (assert) {
    var expected = 100;
    var actual = parseFloat2(getColumnText(itemTable, 1));
    assert.equal(actual, expected, "The function returned expected value \"" + expected + "\".");

    expected = 4000;
    actual = parseFloat2(getColumnText(itemTable, 2));

    assert.equal(actual, expected, "The function returned expected value \"" + expected + "\".");

    expected = 2;
    actual = parseFloat2(getColumnText(itemTable, 4));

    assert.equal(actual, expected, "The function returned expected value\"" + expected + "\".");

});

