MixERPApp.controller("CheckListController", function ($scope, $sce) {
    var ajaxGetFirstSteps = getFirstSteps();

    function getResource() {
        var i18n = new Object();

        i18n.FirstSteps = window.Resources.Titles.FirstSteps();
        i18n.TaskCompletedProgress = window.Resources.Labels.TaskCompletedProgress();
        i18n.Start = window.Resources.Titles.Start();
        i18n.Search = window.Resources.Titles.Search();
        i18n.HideForNow = window.Resources.Titles.HideForNow();

        return i18n;
    };

    ajaxGetFirstSteps.success(function (msg) {
        $scope.$apply(function () {
            $scope.checklist = msg.d;
            $scope.filters = {};
            $scope.i18n = getResource();


            $scope.complete = _.where($scope.checklist, { Status: true }).length;
            $scope.total = $scope.checklist.length;
            $scope.percent = Math.floor(($scope.complete / $scope.total) * 100);
            $scope.documentLocation = document.location.pathname;
            $scope.i18n.TaskCompletedProgress = stringFormat($scope.i18n.TaskCompletedProgress, $scope.complete, $scope.total);

        });

        $scope.removeNonBreakingSpaces();
    });

    $scope.remove = function (item) {
        if (!confirmAction()) {
            return;
        };

        var index = $scope.checklist.indexOf(item);
        $scope.checklist.splice(index, 1);
        $scope.removeNonBreakingSpaces();
    };

    ajaxGetFirstSteps.fail(function (xhr) {
        logAjaxErrorMessage(xhr);
    });

    $scope.removeNonBreakingSpaces = function () {
        window.setTimeout(function () {
            $(".to.whitespace").each(function() {
                $(this).html($(this).html().replace(/&nbsp;/g, " "));
            });
        }, 100);
    };

    $scope.$watch("filters.Category", function () {
        $scope.removeNonBreakingSpaces();
    });

    $scope.$watch("searchByName", function () {
        $scope.removeNonBreakingSpaces();
    });
});

function getFirstSteps() {
    var url = "/Services/Checklists.asmx/GetFirstSteps";
    return getAjax(url);
};
