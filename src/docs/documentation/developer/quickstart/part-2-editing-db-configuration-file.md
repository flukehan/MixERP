#Editing DB Configuration

Edit the following file using Visual Studio or any text editor of your choice.

**src/FrontEnd/MixERP.Net.FrontEnd/Resource/Configuration/DbServer.xml**

```xml
<configuration>
  <appSettings>
    <add key="Server" value="localhost" />
    <add key="Port" value="5432" />
    <add key="Database" value="mixerp" />
    <add key="UserId" value="mix_erp" />
    <add key="Password" value="change-on-deployment" />

    <add key="PostgreSQLBinDirectory" value="C:\Program Files\PostgreSQL\9.3\bin\" />
    <add key="DatabaseBackupDirectory" value="/Resource/Backups/" />
  </appSettings>
</configuration>
```

###Configuration Explained

| Key                         | Configuration |
|-----------------------------| -------------|
| Server                      | The hostname or IP address of your development PostgreSQL Server instance. Usually "localhost". |
| PostgreSQLBinDirectory      | The port on which the PostgreSQL server is listening. Usually "5432". |
| Database                    | The name of the database in which you have installed (run) MixERP database script |
| UserId                      | MixERP database script will create a user mix_erp. Leave it as it is. |
| Password                    | Password for the above user. The default password is "change-on-deployment". If you happen to change the password, change it here as well. |
| PostgreSQLBinDirectory      | Depending upon where you installed PostgreSQL server, provide the correct location of the bin directory. |
| DatabaseBackupDirectory     | Provide the path where you want to keep your database backups. If you host MixERP, make sure that the IIS user has write access to the path. |

##Related Topics
* [Part 1: Installing Database](part-1-installing-database.md)
* [Documentation Home](../../../index.md)