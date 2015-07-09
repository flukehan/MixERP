/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

/*jshint -W032 */

var tranId;
var approve;
var url;
var data;
var selectedIndex;

$(document).ready(function () {
    var reasonTextArea = $("#ReasonTextArea");

    var transactionGridView = $("#TransactionGridView");
    var approveButton = $("#ApproveButton");
    var rejectButton = $("#RejectButton");
    var modal = $("#ActionModal");
    var verifyButton = $("#VerifyButton");

    function getSelectedItem() {
        var selectedControl = transactionGridView.find("input:checked").first();
        selectedIndex = parseInt(selectedControl.parent().parent().parent().index());

        if (selectedControl.length) {
            return parseInt2(selectedControl.parent().parent().parent().find("td:nth-child(3)").html());
        };

        return 0;
    };

    function showModal() {
        var header = modal.find(".ui.massive.header");
        var subheader = modal.find(".ui.dividing.header");

        header.html(Resources.Titles.RejectThisTransaction());
        subheader.html(String.format(Resources.Titles.TranIdParameter(), tranId));
        header.removeClass("green").addClass("red");

        if (approve) {
            header.html(Resources.Titles.ApproveThisTransaction());
            header.removeClass("red").addClass("green");
        };

        modal.modal('setting', 'closable', false).modal('show');
    };

    approveButton.click(function () {
        tranId = getSelectedItem();

        if (tranId) {
            approve = true;
            showModal();
        };
    });

    rejectButton.click(function () {
        tranId = getSelectedItem();

        if (tranId) {
            approve = false;
            showModal();
        };
    });


    function ajaxApprove(tranId, reason) {
        url = "/Modules/Finance/Services/Transactions.asmx/Approve";
        data = appendParameter("", "tranId", tranId);
        data = appendParameter(data, "reason", reason);
        data = getData(data);

        return getAjax(url, data);
    };

    function ajaxReject(tranId, reason) {
        url = "/Modules/Finance/Services/Transactions.asmx/Reject";
        data = appendParameter("", "tranId", tranId);
        data = appendParameter(data, "reason", reason);
        data = getData(data);

        return getAjax(url, data);
    };

    verifyButton.click(function () {
        var reason = reasonTextArea.val();
        var ajaxAction;

        if (approve) {
            ajaxAction = ajaxApprove(tranId, reason);
        } else {
            ajaxAction = ajaxReject(tranId, reason);
        }

        ajaxAction.success(function () {
            transactionGridView.find("tr").eq(selectedIndex + 1).addClass("negative").fadeOut(500, function () {
                $(this).remove();
            });
        });

        ajaxAction.fail(function (xhr) {
            logAjaxErrorMessage(xhr);
        });

        return false;
    });

    $(document).keyup(function (e) {
        if (e.ctrlKey) {
            var rowNumber = e.keyCode - 47;

            if (rowNumber < 10) {
                transactionGridView.find("tr").eq(rowNumber).find("input").trigger('click');
            };
        };
    });

    shortcut.add("CTRL+K", function () {
        approveButton.trigger("click");
    });

    shortcut.add("CTRL+RETURN", function () {
        if (modal.is(":visible")) {
            verifyButton.trigger("click");
        };
    });

    shortcut.add("CTRL+SHIFT+K", function () {
        rejectButton.trigger("click");
    });


});
