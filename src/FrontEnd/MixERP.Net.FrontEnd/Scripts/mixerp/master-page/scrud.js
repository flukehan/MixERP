function pageLoad() {
    initializeItemSelector();
    $(".item-selector").colorbox({ iframe: true, innerWidth: 1024, innerHeight: 450, overlayClose: false });
    $(".preview").colorbox({ iframe: true, innerWidth: 1024, innerHeight: 450, overlayClose: false });
};

if (typeof Sys !== "undefined") {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
        //Fired on each ASP.net AJAX request.
        initializeItemSelector();
    });
};

var initializeItemSelector = function () {
    var itemSelector = $("[role=item-selector]");
    var modalTemplatePath = "/Static/Templates/ModalTemplate.html"; //Todo:parameterize this path

    itemSelector.each(function () {
        var selector = $(this);
        var href = selector.prop("href");
        selector.attr("data-url", href);
        selector.prop("href", "javascript:void(0);");
    });

    itemSelector.click(function () {
        var href = $(this).attr("data-url");
        var title = $(this).attr("data-title");

        $.get(modalTemplatePath + "?v=8", function () { }).done(function (data) {
            var itemSelectorDiv = $(data);
            if (!isNullOrWhiteSpace(title)) {
                itemSelectorDiv.find(".header").html("<i class='help basic icon'></i>" + title);
            };

            $("body").append(itemSelectorDiv);

            itemSelectorDiv.find(".content").html('<div class="ui segment"><iframe width="100%" height="400px" frameborder="0" allowtransparency="true" src="' + href + '"></iframe></div>');
            //itemSelectorDiv.addClass("item-selector-modal");

            itemSelectorDiv.modal('show');
        });
    });
};

var closeItemSelector = function () {
    $('.item-selector-modal').modal('hide');
};
