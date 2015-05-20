///<reference path="../bundles/scripts/master-page.min.js"/>
QUnit.test("localization.js -> getFormattedNumber (Case 1)", function (assert) {
    //Cultures: en-US, zh-CN, fil-PH, ja-JP, ms-MY
    window.currencyDecimalPlaces = 2;
    window.thousandSeparator = ",";
    window.decimalSeparator = ".";

    var expectedNumbers = ['100.00', '1,000.00', '1,000,000,000.00', '1,000,000,000,000,000.00'];
    var numbers = ['100', '1000', '1000000000', '1000000000000000'];

    $.each(numbers, function (i, v) {
        var actual = getFormattedNumber(v);
        var expected = expectedNumbers[i];

        assert.equal(actual, expected, "The function returned " +
            "expected formatted number \"" + expected + "\".");
    });
});

QUnit.test("localization.js -> getFormattedNumber (Case 2)", function (assert) {
    //Cultures: es-ES, pt-PT, nl-NL, de-DE, sv-SE, id_ID
    window.currencyDecimalPlaces = 2;
    window.thousandSeparator = ".";
    window.decimalSeparator = ",";

    var expectedNumbers = ['100,00', '1.000,00', '1.000.000.000,00', '1.000.000.000.000.000,00'];
    var numbers = ['100', '1000', '1000000000', '1000000000000000'];


    $.each(numbers, function (i, v) {
        var actual = getFormattedNumber(v);
        var expected = expectedNumbers[i];

        assert.equal(actual, expected, "The function returned " +
            "expected formatted number \"" + expected + "\".");
    });
});

QUnit.test("localization.js -> getFormattedNumber (Case 3)", function (assert) {
    //Cultures: fr-FR, ru-RU 
    window.currencyDecimalPlaces = 2;
    window.thousandSeparator = " ";
    window.decimalSeparator = ",";

    var expectedNumbers = ['100,00', '1 000,00', '1 000 000 000,00', '1 000 000 000 000 000,00'];
    var numbers = ['100', '1000', '1000000000', '1000000000000000'];


    $.each(numbers, function (i, v) {
        var actual = getFormattedNumber(v);
        var expected = expectedNumbers[i];

        assert.equal(actual, expected, "The function returned " +
            "expected formatted number \"" + expected + "\".");
    });
});

QUnit.test("localization.js -> parseFormattedNumber (Case 1)", function (assert) {
    //Cultures: en-US, zh-CN, fil-PH, ja-JP, ms-MY
    window.thousandSeparator = ",";
    window.decimalSeparator = ".";
    var num = '1,000,000,000,000.00';

    var expected = '1000000000000.00';
    var actual = parseFormattedNumber(num);

  
    assert.equal(actual,expected,"The function returned expected parsed formatted number \"" + expected + "\".");

});

QUnit.test("localization.js -> parseFormattedNumber (Case 2)", function (assert) {
    //Cultures: es-ES, pt-PT, nl-NL, de-DE, sv-SE, id_ID
    window.thousandSeparator = ".";
    window.decimalSeparator = ",";
    var num = '1.000.000.000.000,00';

    var expected = '1000000000000.00';
    var actual = parseFormattedNumber(num);

  
    assert.equal(actual,expected,"The function returned expected parsed formatted number \"" + expected + "\".");

});
QUnit.test("localization.js -> parseFormattedNumber (Case 3)", function (assert) {
    //Cultures: fr-FR, ru-RU 
    window.thousandSeparator = " ";
    window.decimalSeparator = ",";
    var num = '1 000 000 000 000,00';

    var expected = '1000000000000.00';
    var actual = parseFormattedNumber(num);

  
    assert.equal(actual,expected,"The function returned expected parsed formatted number \"" + expected + "\".");

});