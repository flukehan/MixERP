@echo off

"C:\Progra~1\PostgreSQL\9.3\bin\pg_dump.exe" -a -U postgres -t localization.localized_resources --data-only --column-inserts mixerp > "%~dp0"\localization.sql

update-dump.bat

pause

exit


