@echo off

rmdir /S /Q dist
del dist.zip

mkdir dist
mkdir dist\scripts

copy config.json dist
copy setup.bat dist
xcopy scripts dist\scripts


dotnet publish superAwesomeAmazingTourPlanner -c Release -o dist

tar.exe -c -f dist.zip dist