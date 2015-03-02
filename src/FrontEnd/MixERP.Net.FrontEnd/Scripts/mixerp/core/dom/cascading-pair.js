function createCascadingPair(select, input) {
    input.blur(function () {
        selectDropDownListByValue(this.id, select.attr("id"));
    });

    select.change(function () {
        input.val(select.getSelectedValue());
    });
};

var selectDropDownListByValue = function (textBoxId, dropDownListId) {
    var listControl = $("#" + dropDownListId);
    var textBox = $("#" + textBoxId);
    var selectedValue = textBox.val();
    var exists;

    if (isNullOrWhiteSpace(textBox.val())) {
        return;
    };

    if (listControl.length) {
        listControl.find('option').each(function () {
            if (this.value === selectedValue) {
                exists = true;
            }
        });
    }

    if (exists) {
        listControl.val(selectedValue).trigger('change');
    } else {
        textBox.val('').trigger('change');
    }

    triggerChange(dropDownListId);
};