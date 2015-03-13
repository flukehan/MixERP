function getParent() {
    var parent;

    if (window.opener && window.opener.document) {
        parent = window.opener;
    }

    if (parent == undefined) {
        parent = window.parent;
    }

    return parent;
};
