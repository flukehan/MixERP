///<reference path="../bundles/scripts/master-page.min.js"/>
QUnit.test("transaction.js -> convertToDebit", function (assert) {
    var expected = -100;//Negative credit balance is debit
    var actualBalance = 100;

    var actual = convertToDebit(actualBalance);

    assert.equal(actual, expected, "The credit balance was converted to debit balance \"" + actual + "\".");

    expected = -1000.33;
    actualBalance = 1000.33;
    actual = convertToDebit(actualBalance);

    assert.equal(actual, expected, "The credit balance was converted to debit balance \"" + actual + "\".");

    expected = -0.33;
    actualBalance = 0.33;
    actual = convertToDebit(actualBalance);

    assert.equal(actual, expected, "The credit balance was converted to debit balance \"" + actual + "\".");

});