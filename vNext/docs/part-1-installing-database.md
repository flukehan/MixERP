#PostgreSQL Server
You will need to install PostgreSQL Server, 9.3 or higher. Get the latest PostgreSQL server installer here:

[http://www.postgresql.org/download/](http://www.postgresql.org/download/)

#Create a New Database
Create a new PostgreSQL database, name it anything you want. Lowercase database name is preferred without
any special character or symbol.

**Collation**

When you create your database, navigate to the tab "Definition". Make sure that you have the following settings:


| Definition     | Value       | 
| -------------- | ------------| 
| Encoding       | UTF8        |
| Template       | template0   |
| Collation      | C or POSIX  |
| Character Type | C or POSIX  |


![Collation](images/collation.png)

#Install MixERP Database
* Run PgAdmin3, select the newly created database. Click Tools --> Query Tool.
* Open the file "src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/mixerp-incremental-sample.sql" if you want sample data.
* If you do not want sample data, open this file instead of the above "src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/mixerp-incremental-blank-db.sql".