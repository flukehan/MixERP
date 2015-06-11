function getDocHeight(margin) {
    var D = document;
    var height = Math.max(
        Math.max(D.body.scrollHeight, D.documentElement.scrollHeight),
        Math.max(D.body.offsetHeight, D.documentElement.offsetHeight),
        Math.max(D.body.clientHeight, D.documentElement.clientHeight)
    );

    if (margin) {
        height += parseInt2(margin);
    };

    return height;
};

var repaint = function () {
    setTimeout(function () {
        $(document).trigger('resize');
    }, 1000);
};