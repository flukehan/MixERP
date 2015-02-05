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
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
--%>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesByGeographyWidget.ascx.cs"
    Inherits="MixERP.Net.Core.Modules.Sales.Widgets.SalesByGeographyWidget" %>

<asp:PlaceHolder runat="server" ID="Placeholder1"></asp:PlaceHolder>


<script src="//cdnjs.cloudflare.com/ajax/libs/d3/3.5.3/d3.min.js"></script>
<script src="//cdnjs.cloudflare.com/ajax/libs/topojson/1.6.9/topojson.min.js"></script>
<script src="/Scripts/Datamaps/datamaps.all.min.js"></script>
<script src="/Scripts/mixerp/utitlities/country-codes.js"></script>
<script src="/Modules/Sales/Scripts/Widgets/SalesByGeographyWidget.ascx.js"></script>
