function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function updateValue(val) {
    var ctl = getParameterByName('AssociatedControlId');
    $('#' + ctl, parent.document.body).val(val);
    parent.jQuery.colorbox.close();
}


document.onkeydown = function (evt) {
    evt = evt || window.event;
    if (evt.keyCode == 27) {
        top.close();
    }
};