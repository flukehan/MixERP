@echo off

..\..\..\..\..\Utilities\MixERP.Net.Utility.SqlBundler\bin\Debug\MixERP.Net.Utility.SqlBundler.exe ..\..\..\ "db/release-1/update-1" false true

c:\windows\system32\cscript.exe merge-files.vbs mixerp-incremental-sample.sql ..\..\beta-1\v2\mixerp-incremental-sample.sql mixerp.sql
del mixerp.sql
copy mixerp-incremental-sample.sql ..\..\sample-db.sql