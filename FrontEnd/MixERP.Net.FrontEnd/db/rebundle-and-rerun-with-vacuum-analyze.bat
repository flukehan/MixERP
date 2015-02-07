@echo off

cmd.exe /c chcp 1252

call rebundle-db.bat


"C:\Progra~1\PostgreSQL\bin\psql.exe" -U postgres --single-transaction -v ON_ERROR_STOP=1 -d mixerp < "%~dp0"\mixerp.en-US.sql

echo Task completed successfully.

echo Now, running full vacuum with analyze.

"C:\Progra~1\PostgreSQL\9.3\bin\psql.exe" -U postgres -d mixerp -c "VACUUM FULL ANALYZE;"

pause
exit

