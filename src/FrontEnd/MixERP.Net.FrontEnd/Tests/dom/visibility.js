///<reference path="/Scripts/jquery-1.9.1.js" />
///<reference path="/Scripts/mixerp/mixerp-core.js" />
QUnit.test("visibility.js -> triggerClick", function (assert) {
    var el = $("<div style='display:none;'></div>");
    $('body').append(el);

    setVisible(el, true, 0);

    var actual = el.is(":visible");

    assert.equal(actual, true, "The element was unhidden.");

    setVisible(el, false, 0);
    actual = el.is(":visible");

    assert.equal(actual, false, "The element was hidden.");

    setVisible(el, false, 0);

    actual = el.is(":visible");

    assert.equal(actual, false, "The element is still hidden.");

    setVisible(el, true, 0);
    actual = el.is(":visible");

    assert.equal(actual, true, "The element was unhidden again.");
});