#AttachmentFactoryParameter.xml

This configuration file is related to the module [AttachmentFactory](../user-guide/core-concepts/attachment-factory.md).

```xml
<?xml version="1.0"?>

<!--
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
-->

<configuration>
  <appSettings>
    <add key="AttachmentsDirectory" value="~/Resource/Static/Attachments/" />
    <add key="UploadHandlerUrl" value="~/FileUploadHanlder.ashx" />
    <add key="UndoUploadServiceUrl" value="~/FileUploadHanlder.asmx/UndoUpload" />
    <add key="AllowedExtensions" value="jpg,jpeg,gif,png,tif,doc,docx,xls,xlsx,pdf" />
  </appSettings>
</configuration>
```


##AttachmentsDirectory
This instructs attachment factory to upload attachments to the specified directory. 
You must **ensure that the the directory is writable** by  IIS user or IIS Application Pool identity 
or whatever user account that your site is running under.

##UploadHandlerUrl
The attachment factory Javascript Ajax library will upload attachments to this url. Make sure that you
create handler a mapping in [web.config](web.config.md) file which matches this location.

##UndoUploadServiceUrl
The attachment factory Javascript Ajax library will request this url to undo upload. Make sure that you
create handler mapping in [web.config](web.config.md) file which matches this location.

##AllowedExtensions
List of allowed extensions that would be allowed as attachments.


##Related Topics
* [Web Configuration File](web.config.md)
* [Administrator Documentation](../admin.md)