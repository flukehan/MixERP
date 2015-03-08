///<reference path="/Scripts/jquery-1.9.1.js" />
///<reference path="/Scripts/mixerp/mixerp-core.js" />
QUnit.test("checkable.js -> toogleSelection", function (assert) {
    var input = $('<input type="checkbox" checked="checked" />').hide();
    $('body').append(input);

    toogleSelection(input);

    if (input.not(":checked")) {
        assert.ok(true, "Input was unchecked.");
    } else {
        assert.ok(false, "Input was not unchecked.");
    };

    toogleSelection(input);//Check
    toogleSelection(input);//Uncheck
    toogleSelection(input);//Check

    if (input.is(":checked")) {
        assert.ok(true, "Input was checked.");
    } else {
        assert.ok(false, "Input was not checked.");
    };

    toogleSelection(input);//Uncheck
    toogleSelection(input);//Check
    toogleSelection(input);//Uncheck
    toogleSelection(input);//Check
    toogleSelection(input);//Uncheck

    if (input.not(":checked")) {
        assert.ok(true, "Input was unchecked again.");
    } else {
        assert.ok(false, "Input was not unchecked again.");
    };

    toogleSelection(input);//Check
    toogleSelection(input);//Uncheck
    toogleSelection(input);//Check
    toogleSelection(input);//Uncheck
    toogleSelection(input);//Check

    if (input.not(":checked")) {
        assert.ok(true, "Input was checked again.");
    } else {
        assert.ok(false, "Input was not checked again.");
    };

    input.remove();
});