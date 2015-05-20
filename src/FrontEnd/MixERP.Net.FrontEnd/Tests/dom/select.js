///<reference path="../bundles/scripts/master-page.min.js"/>
var select = $("<select id='ItemSelect'/>").hide();
QUnit.test("select.js -> getSelectedText", function (assert) {
    var items = ['Macbook', 'Ipad', 'Mobile', 'Laptop'];
    $.each(items, function (index, value) {
        select.append($('<option/>', {
            value: value,
            text: value
        }));
    });
    $('body').append(select);

    $.each(items, function (index, value) {
        shouldEqualByItemSelect(value, assert, select);
    });
});

function shouldEqualByItemSelect(expected, assert, select) {
    select.val(expected);
    select.trigger('change');

    actual = select.getSelectedText();
    assert.equal(expected, actual, "The function returned the expected value \"" + expected + "\".");


};



