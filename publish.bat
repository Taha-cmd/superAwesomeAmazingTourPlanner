@echo off

rmdir /S /Q dist

mkdir dist
mkdir dist\scripts

copy config.json dist
copy setup.bat dist
xcopy scripts dist\scripts


dotnet publish superAwesomeAmazingTourPlanner -c Release -o dist