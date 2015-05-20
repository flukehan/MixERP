@echo off

cmd.exe /c chcp 1252

call rebundle-db-without-sample.bat


"C:\Progra~1\PostgreSQL\9.4\bin\psql.exe" -U postgres --single-transaction -v ON_ERROR_STOP=1 -d mixerp < "%~dp0"\mixerp.sql

echo Task completed successfully.
pause