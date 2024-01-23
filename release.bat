@echo off

set zipfilename=twohandedcompany.zip
set srcdir=%cd%

"C:\Program Files\WinRAR\winrar.exe" a -ep1 "%zipfilename%" ^
    "%srcdir%\icon.png" ^
    "%srcdir%\README.md" ^
    "%srcdir%\CHANGELOG.md" ^
    "%srcdir%\manifest.json" ^
    "%srcdir%\bin\debug\TwoHandedCompany.dll"

echo Files compressed to %zipfilename%
pause
