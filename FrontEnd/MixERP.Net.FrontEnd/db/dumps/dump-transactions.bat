@Echo OFF

"C:\Progra~1\PostgreSQL\9.3\bin\pg_dump.exe" -a -U postgres -t audit.logins -t core.flags -t core.fiscal_year -t core.frequency_setups -t transactions.* --data-only --column-inserts mixerp > "%~dp0"\..\src\dump.sql
Pause
Exit


