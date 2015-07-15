@echo off
..\..\..\..\..\Utilities\MixERP.Net.Utility.SqlBundler\bin\Debug\MixERP.Net.Utility.SqlBundler.exe ..\..\..\ "db/release-1/update-1" false false

copy mixerp.sql mixerp-patch-for-rc.sql
copy mixerp.sql ..\..\patch.sql

c:\windows\system32\cscript.exe merge-files.vbs mixerp-incremental-blank-db.sql ..\..\beta-1\v2\mixerp-incremental-blank-db.sql mixerp.sql
del mixerp.sql
copy mixerp-incremental-blank-db.sql ..\..\blank-db.sql
