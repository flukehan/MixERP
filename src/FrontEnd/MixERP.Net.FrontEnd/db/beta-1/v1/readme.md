#Important

Do not edit any files inside this directory or sub-directories.

#rebundle-db-with-sample.bat

This batch file will rebundle database scripts with sample data. 
As a result, the file **mixerp-sample.sql** will be created or replaced.


#rebundle-db-without-sample.bat

This batch file will rebundle database scripts without sample data. 
As a result, the file **mixerp-blank.sql** will be created or replaced.


#rebundle-and-rerun-with-sample.bat

This batch file expects database **mixerp** to be present before you execute it. It performs the following tasks:

* calls **rebundle-db-with-sample.bat**
* runs **mixerp-sample.sql** on **mixerp** database.

Before running this batch file, please make sure that you have PostgreSQL 9.4 installed.

#rebundle-and-rerun-without-sample.bat

This batch file expects database **mixerp** to be present before you execute it. It performs the following tasks:

* calls **rebundle-db-without-sample.bat**
* runs **mixerp-blank.sql** on **mixerp** database.

Before running this batch file, please make sure that you have PostgreSQL 9.4 installed.


#rebundle-and-rerun-with-sample-with-vacuum-analyze.bat

This batch file expects database **mixerp** to be present before you execute it. It performs the following tasks:

* calls **rebundle-db-with-sample.bat**
* runs **mixerp-sample.sql** on **mixerp** database.
* runs a full **VACUUM FULL ANALYZE** command on **mixerp** database.

Before running this batch file, please make sure that you have PostgreSQL 9.4 installed.


#rebundle-and-rerun-without-sample-with-vacuum-analyze.bat

This batch file expects database **mixerp** to be present before you execute it. It performs the following tasks:

* calls **rebundle-db-without-sample.bat**
* runs **mixerp-blank.sql** on **mixerp** database.
* runs a full **VACUUM FULL ANALYZE** command on **mixerp** database.

Before running this batch file, please make sure that you have PostgreSQL 9.4 installed.

