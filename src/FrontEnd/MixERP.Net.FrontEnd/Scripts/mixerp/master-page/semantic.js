$(document).ready(function () {
    $('.ui.checkbox').checkbox();
    $('.ui.dropdown').dropdown();
});

//Extending Semantic UI Form Validation Rules
$.extend(true, $.fn.form.settings.rules, {
    not_matches: function (value, fieldIdentifier) {
        // use either id or name of field
        var $form = $(this);
        var matchingValue;

        if ($form.find('#' + fieldIdentifier).length > 0) {
            matchingValue = $form.find('#' + fieldIdentifier).val();
        } else if ($form.find('[name="' + fieldIdentifier + '"]').length > 0) {
            matchingValue = $form.find('[name="' + fieldIdentifier + '"]').val();
        } else if ($form.find('[data-validate="' + fieldIdentifier + '"]').length > 0) {
            matchingValue = $form.find('[data-validate="' + fieldIdentifier + '"]').val();
        }

        return (matchingValue)
            ? (value.toString() !== matchingValue.toString())
            : false;
    }
});