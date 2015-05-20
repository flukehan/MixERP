#Administrator Documentation

This document provides you insight with deploying, installing, and configuring MixERP.

#Deployment Documentation

Please follow the following platform specific deployment information:
* [Deploying MixERP in Windows (IIS)](deployment/iis.md)
* [Deploying MixERP in Windows using Deployment Utility](deployment/deployment-utility.md)

#Configuration

You may need to edit and modify one or several configuration files to suit your needs. It is, therefore,
very important to understand how MixERP uses these configuration files.

<table>
    <thead>
        <tr>
            <th>
                File
            </th>
            <th>
                Path
            </th>
            <th>
                Description/Related Module(s)
            </th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>
                <a href="configs/web.config.md">web.config</a>
            </td>
            <td>
                Application root or sub-directories
            </td>
            <td>
                The main configuration file IIS server uses
            </td>
        </tr>
        <tr>
            <td>
                ApiConfiguration.xml
            </td>
            <td>
                /Resource/Configuration
            </td>
            <td>
                MixERP Restful API
            </td>
        </tr>
        <tr>
            <td>
                <a href="configs/attachment-factory-parameters.xml.md">AttachmentFactoryParameters.xml</a>
            </td>
            <td>
                /Resource/Configuration
            </td>
            <td>
                AttachmentFactory HTML Helper (MVC) or AttachmentFactory Module (WebForms)
            </td>
        </tr>
        <tr>
            <td>
                <a href="configs/db-parameters.xml.md">DbParameters.xml</a>
            </td>
            <td>
                /Resource/Configuration
            </td>
            <td>
                Used by ScrudFactory HTML Helper (MVC) or ScrudFactory Module (WebForms) for
                <a href="../developer/scrud/display-fields.md">DisplayFields.</a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="configs/db-server.xml.md">DbServer.xml</a>
            </td>
            <td>
                /Resource/Configuration
            </td>
            <td>
                Used by several MixERP modules, contains information about your database server
            </td>
        </tr>
        <tr>
            <td>
                <a href="configs/messaging-parameters.xml.md">MessagingParameters.xml</a>
            </td>
            <td>
                /Resource/Configuration
            </td>
            <td>
                Used by <a href="../developer/core/messaging/index.md">MixERP.Net.Messaging</a> module for sending
                or receiving messages.
            </td>
        </tr>
        <tr>
            <td>
                MixERP.xml
            </td>
            <td>
                /Resource/Configuration
            </td>
            <td>
                MixERP core configuration file
            </td>
        </tr>
        <tr>
            <td>
                Parameters.xml
            </td>
            <td>
                /Resource/Configuration
            </td>
            <td>
                MixERP core configuration file
            </td>
        </tr>
        <tr>
            <td>
                ReportParameters.xml
            </td>
            <td>
                /Resource/Configuration
            </td>
            <td>
                Used by ReportingEngine HTML Helper (MVC) or ReportingEngine Module (WebForms)
            </td>
        </tr>
        <tr>
            <td>
                ScrudParameters.xml
            </td>
            <td>
                /Resource/Configuration
            </td>
            <td>
                Used by ScrudFactory HTML Helper (MVC) or ScrudFactory Module (WebForms)
            </td>
        </tr>
        <tr>
            <td>
                Switches.xml
            </td>
            <td>
                /Resource/Configuration
            </td>
            <td>
                Used by several MixERP modules, contains parameters that can be switched on (true) or off(false)
            </td>
        </tr>
        <tr>
            <td>
                TransactionChecklistParameters.xml
            </td>
            <td>
                /Resource/Configuration
            </td>
            <td>
                Used by TransactionsactionChecklist module
            </td>
        </tr>
    </tbody>
</table>


##Related Topics
* [MixERP Documentation](../index.md)