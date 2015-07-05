QUnit.test("validation.js -> isNullOrWhiteSpace", function (assert) {
    var actual = isNullOrWhiteSpace("  ");
    var message = "The function passed the test.";
    assert.equal(true, actual, message);

    actual = isNullOrWhiteSpace("owieru");
    assert.equal(false, actual, message);
    
    actual = isNullOrWhiteSpace(['', '', '1']);
    assert.equal(true, actual, message);

    actual = isNullOrWhiteSpace(['1', 'abx', 'sdf4']);
    assert.equal(false, actual, message);
});

