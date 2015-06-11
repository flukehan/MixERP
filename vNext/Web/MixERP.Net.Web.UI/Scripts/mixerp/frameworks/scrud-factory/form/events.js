function displayForm() {
    $('#' + gridPanelId).hide(500);
    $('#' + formPanelId).show(500);

    if (typeof window.scrudFormDisplayedCallBack === "function") {
        window.scrudFormDisplayedCallBack();
    };
};

var scrudRepaint = function () {
    setTimeout(function () {
        $(document).trigger('resize');
    }, 1000);
};

function save() {
    var validation = scrudClientValidation();

    if (!validation) {
        return;
    };


    var id = scrudGetQueryStringByName("edit");

    if (id) {
        scrudUpdate(id);
        return;
    };

    scrudInsert();
};

function resetForm() {
    $("#" + formPanelId).each(function () {
        this.reset();
    });
};


function scrudDelete() {
    alert("Deleting");
};