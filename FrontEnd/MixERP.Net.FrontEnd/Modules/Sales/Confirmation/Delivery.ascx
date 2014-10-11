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

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Delivery.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Confirmation.Delivery"
    OverridePath="/Modules/Sales/Delivery.mix" %>
<mixerp:TransactionChecklistForm
    id="TransactionCheckList1"
    runat="server"
    attachmentbookname="transaction"
    overridepath="/Modules/Sales/Delivery.mix"
    displaywithdrawbutton="true"
    displayviewreportbutton="true"
    displayemailreportbutton="true"
    displaycustomerreportbutton="true"
    displayprintreceiptbutton="false"
    displayprintglentrybutton="true"
    displayattachmentbutton="true"
    reportpath="~/Modules/Sales/Reports/DeliveryReport.mix"
    customerreportpath="~/Modules/Sales/Reports/DeliveryNoteReport.mix"
    gladvicepath="~/Modules/Finance/Reports/GLAdviceReport.mix"
    viewpath="/Modules/Sales/Delivery.mix"
    addnewpath="/Modules/Sales/Entry/Delivery.mix" />
