@echo off

cmd.exe /c chcp 1252

call rebundle-and-rerun-without-sample.bat

echo Task completed successfully.

echo Now, running full vacuum with analyze.

"C:\Progra~1\PostgreSQL\9.4\bin\psql.exe" -U postgres -d mixerp -c "VACUUM FULL ANALYZE;"

exit

