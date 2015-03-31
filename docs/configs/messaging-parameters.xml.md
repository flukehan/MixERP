#MessagingParameters.xml

This configuration file is used by Messaging module to send and receive messages.

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
along with MixERP.  If not, see <http://www.gnu.org/licenses />.
-->

<configuration>
  <appSettings>
    <add key="FromDisplayName" value="MixERP" />
    <add key="FromEmailAddress" value="mixerp@localhost" />
    <add key="SmtpDeliveryMethod" value="SpecifiedPickupDirectory" />
    <add key="SpecifiedPickupDirectoryLocation" value="~/Resource/Static/Emails" />
    <add key="SMTPHost" value="smtp-mail.outlook.com" />
    <add key="SMTPPort" value="587" />
    <add key="SMTPEnableSSL" value="true" />
    <add key="SMTPUserName" value="" />
    <add key="SMTPPassword" value="" />
    <add key="" value="" />
  </appSettings>
</configuration>
```



##Related Topics
* [Administrator Documentation](../admin.md)