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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportWriter.ascx.cs" Inherits="MixERP.Net.Core.Modules.BackOffice.Admin.ReportWriter" %>
<style type="text/css">
    .ui.form textarea {
        max-height: 1200px;
    }

    .full.height.form, .full.height.form .field, .full.height.form .field textarea {
        height: 100%;
    }
</style>

<h1>Report Writer</h1>
<div class="ui blue buttons">
    <button class="ui button" onclick="showReports();">
        <i class="folder open icon"></i>Open
    </button>
    <button class="ui button" id="SaveButton">
        <i class="save icon"></i>Save
    </button>
    <button class="ui button">
        <i class="remove icon"></i>Close
    </button>
    <button class="ui button">
        <i class="minus circle icon"></i>Delete
    </button>
</div>

<div class="ui divider"></div>
<div class="ui top attached tabular menu">
    <a class="item active" data-tab="first">
        <i class="database icon"></i>Data Sources
    </a>
    <a class="item" data-tab="second">
        <i class="chevron up icon"></i>Top Section
    </a>
    <a class="item" data-tab="third">
        <i class="list layout icon"></i>Report Body
    </a>
    <a class="item" data-tab="fourth">
        <i class="chevron down icon"></i>Bottom Section
    </a>
    <a class="item" data-tab="fifth">
        <i class="settings icon"></i>Setup
    </a>
</div>
<div class="ui bottom attached tab segment active" data-tab="first" style="overflow: auto;">
    <div class="ui buttons">
        <button class="ui button" onclick=" addDataSource(); ">
            <i class="plus circle icon"></i>
            Add
        </button>
        <button class="ui button" onclick=" saveDataSource(); ">
            <i class="save icon"></i>
            Save
        </button>
        <button class="ui button" onclick=" showPreviousDataSource(); ">
            <i class="arrow circle left icon"></i>
            Previous
        </button>
        <button class="ui button" onclick=" showNextDataSource() ">
            <i class="arrow circle right icon"></i>
            Next
        </button>
        <button class="ui button">
            <i class="minus circle icon"></i>Delete
        </button>
    </div>
    <div class="ui divider"></div>
    <div class="ui form">
        <div class="field">
            <label>Query</label>
            <textarea id="QueryTextArea" style="height: 220px;"></textarea>
        </div>
        <div class="four fields">
            <div class="running-total-fields field">
                <label>Running Total Fields (Indices)</label>
                <input type="text" />
            </div>
            <div class="running-total-text field">
                <label>Running Total Text Footer Index</label>
                <input type="text" />
            </div>
        </div>
        <div id="Parameters">
        </div>
        <div class="">
            <button class="ui red button" onclick=" testDataSource(); ">
                <i class="lightning icon"></i>Test
            </button>
        </div>

        <div class="ui text right vpad8">
            <div class="ui datasource label">
                Data Source: <span class="current">1</span> of <span class="total">1</span>
            </div>
        </div>
    </div>
</div>
<div class="ui bottom attached tab segment" data-tab="second">
    <div class="full height ui form">
        <div class="full height field">
            <textarea onblur=" setActiveElement(this); " id="TopSectionTextArea"></textarea>
        </div>
    </div>
</div>

<div class="ui bottom attached tab segment" data-tab="third">
    <div class="full height ui form">
        <div class="field">
            <textarea onblur=" setActiveElement(this); " id="BodyTextArea"></textarea>
        </div>
    </div>
</div>
<div class="ui bottom attached tab segment" data-tab="fourth">
    <div class="full height ui form">
        <div class="field">
            <textarea onblur=" setActiveElement(this); " id="BottomSectionTextArea"></textarea>
        </div>
    </div>
</div>
<div class="ui bottom attached tab segment" data-tab="fifth">
    <div class="ui form" style="width: 400px;">
        <div class="field">
            <label>Report Title</label>
            <input type="text" id="TitleInputText" />
        </div>
        <div class="field">
            <label>Report File Name</label>
            <input type="text" id="FileNameInputText" />
        </div>
        <div class="field">
            <label>Menu Code</label>
            <input type="text" id="MenuCodeInputText" />
        </div>
        <div class="field">
            <label>Parent Menu Code</label>
            <input type="text" id="ParentMenuCodeInputText" />
        </div>
    </div>
</div>
<div class="ui blue buttons vpad16">
    <button class="ui button" onclick=" $('#GridModal').modal('show'); ">
        <i class="grid layout icon"></i>Grid
    </button>
    <button class="ui button" onclick=" $('#FieldModal').modal('show'); ">
        <i class="plus icon"></i>Field
    </button>
    <button class="ui button" onclick=" $('#ResourceModal').modal('show'); ">
        <i class="translate icon"></i>Resource
    </button>
    <button class="ui button" onclick=" insertTable(); ">
        <i class="table icon"></i>Table
    </button>
</div>


<div class="ui large modal" id="PreviewModal" style="overflow: auto;">
    <div class="ui teal header">
        Result Window (Data Source #<span id="data-source"></span>)
    </div>
    <div class="content">
        <div id="GridPanel">
        </div>
    </div>
</div>

<div class="ui small modal" id="GridModal" style="overflow: auto;">
    <div class="ui teal header">
        GridViews
    </div>
    <div class="content">
        <div class="ui form">
            <div class="two fields">
                <div class="field">
                    <label>Data Source Index</label>
                    <select id="DataSourceIndexSelect"></select>
                </div>
                <div class="field">
                    <label>Add GridView</label>
                    <div class="ui checkbox">
                        <input type="checkbox" id="AddGridViewCheckBox" />
                        <label>Yes</label>
                    </div>
                </div>
            </div>
            <div class="two fields">
                <div class="field">
                    <label>CSS Class</label>
                    <input type="text" id="GridViewCssClassInputText">
                </div>
                <div class="field">
                    <label>CSS Style</label>
                    <input type="text" id="GridViewStyleInputText">
                </div>
            </div>
            <button class="ui small positive button" id="SaveGridViewButton">Save</button>
        </div>
    </div>
</div>

<div class="ui small modal" id="FieldModal" style="overflow: auto;">
    <div class="ui teal header">
        Add a Data Bound Field
    </div>
    <div class="content">
        <div class="ui form">
            <div class="two fields">
                <div class="field">
                    <label>Data Source Index</label>
                    <select id="FieldDataSourceIndexSelect"></select>
                </div>
                <div class="field">
                    <label>Field Name</label>
                    <input type="text" id="FieldNameInputText">
                </div>
            </div>
            <button class="ui small positive button" id="AddFieldButton">Add</button>
        </div>
    </div>
</div>

<div class="ui small modal" id="ResourceModal" style="overflow: auto;">
    <div class="ui teal header">
        Add a Localized Resource
    </div>
    <div class="content">
        <div class="ui form">
            <div class="two fields">
                <div class="field">
                    <label>Resource Class Name</label>
                    <input type="text" id="ResourceClassNameInputText" />
                </div>
                <div class="field">
                    <label>Resource Name</label>
                    <input type="text" id="ResourceNameInputText">
                </div>
            </div>
            <button class="ui small positive button" id="AddResourceButton">Add</button>
        </div>
    </div>
</div>

<div class="ui large modal" id="ReportModal">
    <i class="close icon"></i>
    <div class="header">
        Open a Report
    </div>
    <div class="content">
        <table class="ui table">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>CreatedOn</th>
                    <th>LastAccessedOn</th>
                    <th>LastWrittenOn</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    <div class="actions">
        <div class="ui black button">Close</div>
    </div>
</div>

<script src="/Modules/BackOffice/Scripts/Admin/ReportWriter.ascx..js"></script>
