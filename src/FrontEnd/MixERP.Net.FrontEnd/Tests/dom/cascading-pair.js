///<reference path="/Scripts/jquery-1.9.1.js" />
///<reference path="/Scripts/mixerp/mixerp-core.js" />
QUnit.test("cascading-pair.js -> createCascadingPair", function (assert) {
    var destinations = ['Mugu', 'Langtang', 'Annapurna', 'Pumori', 'Denver', 'Las Vegas'];
    var select = $("<select  id='DesintaionSelect' />").hide();
    var input = $("<input type='text' id='DestinationInput' />").hide();

    $.each(destinations, function (index, value) {
        select.append($('<option/>', {
            value: value,
            text: value
        }));
    });

    //Append the pair to document
    $('body').append(select).append(input);


    createCascadingPair(select, input);



    destinations = destinations.sort(function () { return 0.5 - Math.random() });

    $.each(destinations, function (index, value) {
        window.shouldEqualByInput(value, assert, input, select);
    });

    destinations = destinations.sort(function () { return 0.5 - Math.random() });

    $.each(destinations, function (index, value) {
        window.shouldEqualBySelect(value, assert, input, select);
    });

    destinations = ['Mississippi', 'Nilgiri', 'Lhotse', 'Texas', 'Colorado', 'St. Austin'];

    $.each(destinations, function (index, value) {
        window.shouldNotEqualByInput(value, assert, input, select);
    });

    destinations = destinations.sort(function () { return 0.5 - Math.random() });

    $.each(destinations, function (index, value) {
        window.shouldNotEqualBySelect(value, assert, input, select);
    });

    select.remove();
    input.remove();
});


function shouldEqualByInput(expected, assert, input, select) {
    input.val(expected);
    input.trigger('blur');

    var actual = select.find("option:selected").val();

    assert.equal(expected, actual, "The pair cascaded on input event to value \"" + expected + "\".");
};

function shouldNotEqualByInput(unexpected, assert, input, select) {
    input.val(unexpected);
    input.trigger('blur');

    var actual = select.find("option:selected").val();

    assert.notEqual(unexpected, actual, "The pair did not cascade on input event to an invalid value \"" + unexpected + "\".");
};

function shouldEqualBySelect(expected, assert, input, select) {
    select.val(expected);
    select.trigger('change');

    actual = input.val();

    assert.equal(expected, actual, "The pair cascaded on select event to value \"" + expected + "\".");
};

function shouldNotEqualBySelect(unexpected, assert, input, select) {
    select.val(unexpected);
    select.trigger('change');

    actual = input.val();

    assert.notEqual(unexpected, actual, "The pair did not cascade on select event to an invalid value \"" + unexpected + "\".");
};
