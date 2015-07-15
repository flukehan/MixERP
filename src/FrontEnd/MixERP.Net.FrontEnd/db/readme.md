#How Are the Database Scripts Organized?

Database script files are bundled as described in this documentation:

http://www.mixerp.org/documentation/database/sample-data

#What Should I Know?

* You do not need to run multiple SQL files to create MixERP database. You just have to choose one correct file 
  (out of several files) to run. Correct means what is correct for  you.
* Each new version of MixERP contains incremental database scripts. 
  This means when you run a script of V2, everything from V1 is included there, except for the exception 
  which is pointed below.
* Each new version contains a special SQL file which contains the word "patch" in it. For example, 
  ```beta-1\v2\mixerp-patch-for-v1.sql``` only contains the required script to upgrade your existing database
  to current version without loosing any data.

#What Should I Remember?
* **Incremental blank SQL script** is blank database generation script of current version which includes past versions as well.
* **Incremental sample SQL script** is sample database generation script of current version which includes past versions as well.
* **Patch SQL script** upgrdes past versions of database

#Remember
These are the latest database scripts:

* blank-db.sql (for creating a blank database)
* sample-db.sql (for creating a sample database)
* patch.sql (for updating the previous database version)