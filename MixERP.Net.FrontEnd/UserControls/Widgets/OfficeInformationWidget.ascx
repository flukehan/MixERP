<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OfficeInformationWidget.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.Widgets.OfficeInformationWidget" %>
<div class="panel">
    <div class="panel-title">
        Office Information (Todo)
    </div>
    <div class="panel-content">
        Your Office : PES-NY-MEM (Memphis Branch)
                    <br />
        Logged in to : PES-NY-BK (Brooklyn Branch)
                    <br />
        Last Login IP : 192.168.0.200
                <br />
        Last Login On : <%=DateTime.Now.ToString(MixERP.Net.Common.Helpers.LocalizationHelper.GetCurrentCulture()) %>
        <br />
        Current Login IP : 192.168.0.200
                <br />
        Current Login On: <%=DateTime.Now.ToString(MixERP.Net.Common.Helpers.LocalizationHelper.GetCurrentCulture()) %>
        <br />
        Role : ADM (Administrators)
                    <br />
        Department : ITD (IT Department)
    </div>
</div>
