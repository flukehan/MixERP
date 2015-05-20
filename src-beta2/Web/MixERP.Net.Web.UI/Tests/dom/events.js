///<reference path="/Scripts/jquery-1.9.1.js" />
///<reference path="/Scripts/mixerp/mixerp-core.js" />
QUnit.test("event.js -> triggerClick", function (assert) {
    var button = $("<input type='button' id='TestButton' />").hide();
    $('body').append(button);

    var actual = false;

    button.click(function () {
        actual = true;
    });

    triggerClick('TestButton');

    assert.ok(true === actual, "Successfully triggered the event 'triggerClick'.");
    button.remove();
});


QUnit.test("event.js -> triggerChange", function (assert) {
    var select = $("<select id='TestSelect' />").hide();
    $('body').append(select);

    var actual = false;

    select.change(function () {
        actual = true;
    });

    triggerChange('TestSelect');

    assert.ok(true === actual, "Successfully triggered the event 'triggerChange'.");
    select.remove();
});