////Widget Support
$('#sortable-container').sortable({ placeholder: "ui-state-highlight", helper: 'clone', handle: 'h2' });

//$(document).ready(function () {
//    var height = 0;
//    $('#sortable-container').each(function () {
//        var items = $(this).find(".widget");

//        items.each(function () {
//            if ($(this).height() > height) {
//                height = $(this).height();
//            }
//        });

//        var margin = 0;

//        items.each(function () {
//            $(this).find(".segment").css("height", height - margin + "px");
//        });
//    });
//});
