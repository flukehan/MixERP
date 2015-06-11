@echo off

bundler\MixERP.Net.Utility.SqlBundler.exe ..\..\..\ "db/beta-1/v2" false true

cscript merge-files.vbs mixerp-incremental-sample.sql ..\v1\mixerp-sample.sql mixerp.sql
del mixerp.sql
pause