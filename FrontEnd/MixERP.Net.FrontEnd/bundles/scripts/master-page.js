///#source 1 1 /Scripts/mixerp/master-page/declaration.js
var contentRow = $("#ContentRow");
var anchors = $("a.sub-menu-anchor");
var footer = $("footer");
var mainContent = $("#MainContent");
var menuContainer = $("#MenuContainer");
var fullWidthContainer = $(".full.width");
var topMenu = $("#TopMenu");
///#source 1 1 /Scripts/mixerp/master-page/js-tree.js
$(function () {
    var tree = $("#tree");

    tree.jstree({
        "plugins": ["wholerow", "search", "types"],
        "types": {
            "default": {
                "icon": "open folder icon"
            },
            "file": {
                "icon": "file icon"
            },
            "active": {
                "icon": "star icon"
            }
        },
        "search": {
            "case_insensitive": true,
            "show_only_matches": true
        },
    }).bind("select_node.jstree", function (e, data) {
        var href = data.node.a_attr.href;
        document.location.href = href;
    }).bind("hover_node.jstree", function (e, data) {
        if (data.node.data) {
            var menuCode = data.node.data.menucode;
            searchInput.val(menuCode);
        }
    }).bind("ready.jstree", function (e, data) {
        var depth = 2;

        $.each(data.instance._model.data, function (i, v) {
            var liClass;
            var dataSelected = false;

            if (v.li_attr && v.li_attr.class) {
                liClass = v.li_attr.class.toString();
            };

            if (v.data && v.data.selected) {
                if (v.data.selected.toString() === "true") {
                    dataSelected = true;
                    if (v.li_attr && v.li_attr.class) {
                        v.li_attr.class = v.li_attr.class.toString() + " jstree-selected";
                    };
                };
            };

            if (v.parents.length <= depth) {
                data.instance._open_to(i);
            };

            if (dataSelected) {
                data.instance._open_to(i);
                return;
            };

            if (liClass === "expanded") {
                data.instance._open_to(i);
                return;
            };
        });

        tree.toggle();
    });

    var to = false;
    searchInput.keyup(function (e) {
        if (to) {
            clearTimeout(to);
        }
        to = setTimeout(function () {
            var v = searchInput.val();
            $('#tree').jstree(true).search(v);
            var result = $(".jstree-search");
            result.prevAll(".jstree-wholerow").addClass("jstree-clicked");

            if (result.length >= 1) {
                if (e.which === 13) {
                    var href = result.first().attr("href");
                    if (href) {
                        window.location = href;
                    };
                };
            };
        }, 250);
    });
});

///#source 1 1 /Scripts/mixerp/master-page/keyboard.js
//Swallow the CTRL + P combination
jQuery(document).bind("keyup keydown", function (e) {
    if (e.ctrlKey && e.keyCode == 80) {
        e.preventDefault();
        return false;
    }

    return true;
});

$(document).ready(function () {
    shortcut.add("CTRL+M", function () {
        searchInput.focus();
    });

    shortcut.add("CTRL+,", function () {
        $("ul").find("[data-selected='true']").each(function () {
            $(this).find("a").focus();
        });
    });

    searchInput.focus();
});

///#source 1 1 /Scripts/mixerp/master-page/layout.js
(function () {
    fixLayout();
})();

$(window).on('resize', function () {
    fixLayout();
});

function fixLayout() {
    if (contentRow) {
        var contentHeight = contentRow.height();
        var docHeight = $(document).height();
        var footerHeight = footer.height();
        var margin = 0;

        if (contentHeight < docHeight - footerHeight - margin) {
            contentRow.css("height", docHeight - footerHeight - margin + "px");
        };
    };

    if (topMenu) {
        if (mainContent) {
            mainContent.css("width", topMenu.width() + "px");
        };

        if (fullWidthContainer) {
            fullWidthContainer.css("width", topMenu.width() + "px");
        };
    };
};
///#source 1 1 /Scripts/mixerp/master-page/scrud.js
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

///#source 1 1 /Scripts/mixerp/master-page/sortable.js
//Widget Support
$('#sortable-container').sortable({ placeholder: "ui-state-highlight", helper: 'clone', handle: 'h2' });

$(document).ready(function () {
    var height = 0;
    $('#sortable-container').each(function () {
        var items = $(this).find(".widget");

        items.each(function () {
            if ($(this).height() > height) {
                height = $(this).height();
            }
        });

        var margin = 0;

        items.each(function () {
            $(this).find(".segment").css("height", height - margin + "px");
        });
    });
});

///#source 1 1 /Scripts/mixerp/master-page/semantic.js
$(document).ready(function () {
    $('.ui.checkbox').checkbox();
});
