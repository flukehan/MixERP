@Echo OFF

"C:\Progra~1\PostgreSQL\9.4\bin\pg_dump.exe" -a -U postgres -t audit.logins -t core.flags -t transactions.* -T transactions.routines --data-only --column-inserts mixerp > "%~dp0"\..\src\99.sample-data\65.dump.sql.sample

CALL update-dump.bat

Pause
Exit


