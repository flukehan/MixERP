var menus;
var depth = 2;
var sidebar = $('.sidebar');
var wrapper = $('#page-wrapper');

$(document).ready(function () {
    adjustSidebar();
    var topMenu = $("#top-menu");
    var resetMenu = $("#reset-menu");
    var menuId = 0;

    if (window.supportsBrowserStorage()) {
        menuId = parseInt(localStorage["menuId"] || 0);
    };

    var ajaxMenu = getAjaxMenu();

    ajaxMenu.success(function (msg) {
        menus = JSON.parse(msg.d);
        loadMenu(topMenu);
        loadTree(menuId, createTree);
    });

    resetMenu.click(function () {
        $(this).addClass('initially hidden');
        window.depth = 2;
        loadTree(0, createTree);
    });
});


function loadMenu(appendTo) {
    var anchors = "";

    $.each(menus, function (i, v) {
        var anchor = "<a class='item' href='javascript:void(0);' onclick='javascript:loadTree(%s, createTree);'>%s</a>";
        anchor = sprintf(anchor, v.Menu.MenuId, v.Menu.MenuText);

        anchors += anchor;
    });

    appendTo.prepend(anchors);
};

function createTree() {
    var tree = $("#tree");

    var searchInput = $("#SearchInput");


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
}

function loadTree(menuId, callback) {
    $("#tree").remove();
    $(".sidebar").append("<div id='tree' style='display:none;'><ul id='treeData'></ul></div>");

    var tree = $("#tree");
    var treeData = tree.find("ul");

    if (window.supportsBrowserStorage()) {
        localStorage["menuId"] = menuId;
    };

    $.each(menus, function (i, v) {
        var items;
        var li;

        if (menuId === 0) {
            items = getItems(v.Children);
            li = sprintf("<li id='node%s'>" +
                "<a id='anchorNode%1$s' href='javascript:void(0);' title='%s'>%2$s</a>" +
                "%s" +
                "</li>",
                v.Menu.MenuId,
                v.Menu.MenuText,
                items);
            treeData.append(li);
        } else {
            if (v.Menu.MenuId === menuId) {
                items = getItems(v.Children);
                li = sprintf("<li id='node%s'>" +
                    "<a id='anchorNode%1$s' href='javascript:void(0);' title='%s'>%2$s</a>" +
                    "%s" +
                    "</li>",
                    v.Menu.MenuId,
                    v.Menu.MenuText,
                    items);
                treeData.append(li);

                $(".remove.icon").parent().removeClass("initially hidden");

                depth = 3;
            };
        };
    });


    if (typeof callback === "function") {
        callback();
    };
};

function getItems(nav) {
    var menu = "";
    $.each(nav, function (i, v) {
        if (i === 0) {
            menu += "<ul>";
        }

        var childMenu = getItems(v.Children);
        var id = v.Menu.MenuId.toString();

        var url = isNullOrWhiteSpace(v.Menu.Url)
            ? "javascript:void(0);"
            : v.Menu.Url.replace("~", "");


        var cssClass = "";
        var dataJsTree = "";
        var dataSelected = "";

        if (isNullOrWhiteSpace(childMenu)) {
            dataJsTree = "data-jstree='{\"type\":\"file\"}'";
        }

        if (url === location.pathname) {
            dataSelected = "data-selected='true'";
            cssClass = "class='expanded'";
            dataJsTree = "data-jstree='{\"type\":\"active\"}'";
        }

        var anchor = "<a href='" + url + "'>" + v.Menu.MenuText + "</a>";

        menu += sprintf("<li id='node%s' %s %s data-menucode='%s' %s>",
            id, cssClass, dataSelected, v.Menu.MenuCode, dataJsTree);


        menu += anchor;
        menu += childMenu;
        menu += "</li>";

        if (i === nav.length - 1) {
            menu += "</ul>";
        }
    });

    return menu;
};

function getAjaxMenu() {
    var url = "/Services/Menu.asmx/GetMenus";
    return getAjax(url);
};

function toggleSidebar(el) {
    if (sidebar.is(":visible")) {
        wrapper.css('margin-left', '0');
    } else {
        wrapper.css('margin-left', '250px');
    };

    sidebar.toggle(100);
};

function adjustSidebar(){
    if ($(document).width() < 800) {
        wrapper.css('margin-left', '0');
        sidebar.hide(100);
        return;
    };

    wrapper.css('margin-left', '250px');
    sidebar.show(100);
    return;
};


window.onresize = function (event) {
    adjustSidebar();
};
