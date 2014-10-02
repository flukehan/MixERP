<%--
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
along with MixERP.  If not, see <http://www.gnu.org/licenses />.
--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Return.ascx.cs" Inherits="MixERP.Net.Core.Modules.Purchase.Confirmation.Return"
    OverridePath="/Modules/Purchase/Return.mix" %>

<mixerp:TransactionChecklist
    ID="TransactionCheckList1"
    runat="server"
    AttachmentBookName="transaction"
    OverridePath="/Modules/Purchase/Return.mix"
    DisplayWithdrawButton="false"
    DisplayViewReportButton="true"
    DisplayEmailReportButton="true"
    DisplayCustomerReportButton="false"
    DisplayPrintReceiptButton="false"
    DisplayPrintGlEntryButton="true"
    DisplayAttachmentButton="true"
    ReportPath="~/Modules/Purchase/Reports/PurchaseReturnReport.mix"
    GlAdvicePath="~/Modules/Finance/Reports/GLAdviceReport.mix" />