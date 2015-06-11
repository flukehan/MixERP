function getFields() {
    var fields = [];

    $("[data-scrud]").each(function () {
        var el = $(this);
        var scrud = el.data("scrud");
        var columnName = $(this).attr("id");
        var value = $(this).val();

        if (scrud === "radio") {
            value = el.find("input[type=radio]:checked").val() === "yes";
        };

        if (!el.is("[readonly]")) {
            var field = new Object();
            field.Key = columnName;
            field.Value = value;

            fields.push(field);
        };
    });

    return fields;
};