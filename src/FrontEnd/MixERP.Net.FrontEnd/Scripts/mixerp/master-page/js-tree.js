$(function () {
    var searchInput = $("#SearchInput");

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
        }
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
