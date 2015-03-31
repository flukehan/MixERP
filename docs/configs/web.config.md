#Web Configuration File (web.config)

Web.config is the default configuration file for all IIS sites. It is an XML document that provides numerous
configuration possibilities for a web application, however that is out of the scope of this document.

Let's dive deeper into the required sections and settings related to MixERP application configuration.

##No Viewstate

Yes, that's right! No viewstate.

```xml
<system.web>
    <pages enableViewState="false" enableViewStateMac="false" enableEventValidation="false" />
</system.web>
```


##No Dynamic ClientID

Turn off the magic HTML ClientID's.

```xml
<system.web>
    <pages clientIDMode="Static" />
</system.web>
```



##Configure IIS to Serve .backup Files

MixERP creates database backups using [custom pg_dump format](http://www.postgresql.org/docs/9.4/static/backup-dump.html)
which uses **zlib compression library**. Since IIS has no clue about **.backup** file extension, an administrator
has to ensure that the backup files are properly being served as static contents.

**Create a static file handler**

```xml
<system.webServer>
    <handlers>
        <add name="StaticHandler" verb="*" path="*.backup" type="System.Web.StaticFileHandler" preCondition="integratedMode" />
    </handlers>
</system.webServer>
```

**Create a Mime Map**

```xml
<system.webServer>
    <staticContent>
        <mimeMap fileExtension=".backup" mimeType="application/octet-stream" />
    </staticContent>
</system.webServer>
```

<div class="alert-box scrud radius">
    This document assumes that your organization's policy allows backups to be downloaded using an internet
    navigator. If your organization does not allow backup files to be served via http(s), you may have to undertake extra steps
    to configure that, which is out of the scope of this document.
</div>


##Register AttachmentFactory Upload Handlers and Services

AttachmentFactory exposes a couple of HTTP handlers and WebServices.

**HTTP Handler**

``MixERP.Net.WebControls.AttachmentFactory.FileUploadHanlder``

**XML Web Service**

``MixERP.Net.WebControls.AttachmentFactory.Handlers.UploadService``

**Create Handlers**

```xml
<system.webServer>
    <handlers>
        <add name="FileUploadHanlder" path="FileUploadHanlder.ashx" verb="*" type="MixERP.Net.WebControls.AttachmentFactory.FileUploadHanlder, MixERP.Net.WebControls.AttachmentFactory, Version=1.0.0.0, Culture=neutral" />
        <add name="FileUploadService" path="FileUploadHanlder.asmx" verb="*" type="MixERP.Net.WebControls.AttachmentFactory.Handlers.UploadService, MixERP.Net.WebControls.AttachmentFactory, Version=1.0.0.0, Culture=neutral" preCondition="integratedMode" />      
    </handlers>
</system.webServer>
```

<div class="alert-box scrud radius">
    <p>
        You need to instruct IIS to serve the above handlers on the paths you configured in 
        <a href="attachment-factory-parameters.xml.md">AttachmentFactoryParameters.xml</a>, namely:
    </p>
    <ul>
        <li>UploadHandlerUrl</li>
        <li>UndoUploadServiceUrl</li>
    </ul>
</div>


##Configure AppSettings

The **appSettings** section is the one and only place that contains information about other configuration files.

```xml
<appSettings>
    <add key="MaxInvalidPasswordAttempts" value="10" />
    <add key="DisplayErrorDetails" value="true" />
    <add key="APIConfigFileLocation" value="/Resource/Configuration/ApiConfiguration.xml" />
    <add key="MixERPConfigFileLocation" value="/Resource/Configuration/MixERP.xml" />
    <add key="DbServerConfigFileLocation" value="/Resource/Configuration/DbServer.xml" />
    <add key="PartyControlConfigFileLocation" value="/Resource/Configuration/PartyControlParameters.xml" />
    <add key="TransactionChecklistConfigFileLocation" value="/Resource/Configuration/TransactionChecklistParameters.xml" />
    <add key="AttachmentFactoryConfigFileLocation" value="/Resource/Configuration/AttachmentFactoryParameters.xml" />
    <add key="DBParameterConfigFileLocation" value="/Resource/Configuration/DbParameters.xml" />
    <add key="MessagingParameterConfigFileLocation" value="/Resource/Configuration/MessagingParameters.xml" />
    <add key="ParameterConfigFileLocation" value="/Resource/Configuration/Parameters.xml" />
    <add key="ReportConfigFileLocation" value="/Resource/Configuration/ReportParameters.xml" />
    <add key="ScrudConfigFileLocation" value="/Resource/Configuration/ScrudParameters.xml" />
    <add key="SwitchConfigFileLocation" value="/Resource/Configuration/Switches.xml" />
</appSettings>
```

**MaxInvalidPasswordAttempts**

Maximum number of invalid password attempts, after which no more sign in attempt will be allowed.

**DisplayErrorDetails**

Display detailed error information. Turn this off (false) on production site.

**APIConfigFileLocation .. SwitchConfigFileLocation**

The path of configuration files relative to web application root directory.


##Related Topics
* [Administrator Documentation](../admin.md)