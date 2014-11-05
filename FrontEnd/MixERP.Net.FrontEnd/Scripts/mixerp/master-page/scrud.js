function pageLoad() {
    $(".item-selector").colorbox({ iframe: true, innerWidth: 1024, innerHeight: 450, overlayClose: false });
    $(".preview").colorbox({ iframe: true, innerWidth: 1024, innerHeight: 450, overlayClose: false });
};

if (typeof Sys !== "undefined") {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
        //Fired on each ASP.net AJAX request.
        initializeItemSelector();
    });
};

$(document).ready(function () {
    initializeItemSelector();
});

var initializeItemSelector = function () {
    var itemSelector = $("[role=item-selector]");
    var modalTemplatePath = "/Resource/Static/Templates/ModalTemplate.html"; //Todo:parameterize this path

    itemSelector.each(function () {
        var selector = $(this);
        var href = selector.prop("href");
        selector.attr("data-url", href);
        selector.prop("href", "javascript:void(0);");
    });

    itemSelector.click(function () {
        var href = $(this).attr("data-url");
        var title = $(this).attr("data-title");

        $.get(modalTemplatePath, function () { }).done(function (data) {
            var itemSelectorDiv = $(data);

            if (!isNullOrWhiteSpace(title)) {
                itemSelectorDiv.find(".modal-title").html(title);
            };

            $("body").append(itemSelectorDiv);

            itemSelectorDiv.find(".modal-body").html('<iframe width="100%" height="100%" frameborder="0" allowtransparency="true" src="' + href + '"></iframe>');
            itemSelectorDiv.addClass("item-selector-modal");

            itemSelectorDiv.modal('show');
        });
    });
};

var closeItemSelector = function () {
    $('.item-selector-modal').modal('hide');
};
