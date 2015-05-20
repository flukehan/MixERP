@Echo OFF

"C:\Progra~1\PostgreSQL\9.3\bin\pg_dump.exe" -a -U postgres -t audit.logins -t core.flags -t transactions.* -t localization.* -T localization.cultures -T transactions.routines --data-only --column-inserts mixerp > "%~dp0"\..\src\99.sample-data\65.dump.sql.sample

Pause
Exit


