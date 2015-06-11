@echo off
bundler\MixERP.Net.Utility.SqlBundler.exe ..\..\..\ "db/beta-1/v2" false false

copy mixerp.sql mixerp-patch-for-v1.sql

cscript merge-files.vbs mixerp-incremental-blank-db.sql ..\v1\mixerp-blank.sql mixerp.sql
del mixerp.sql