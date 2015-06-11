if (!String.prototype.format) {
// ReSharper disable once NativeTypePrototypeExtending
    String.prototype.format = function () {
        var args = arguments;
        return this.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] !== 'undefined'
                ? args[number]
                : match;
        });
    };
};