@echo off

rmdir /S /Q dist
del dist.zip

mkdir dist
mkdir dist\scripts
mkdir dist\Assets

copy config.json dist
copy setup.bat dist
xcopy scripts dist\scripts
xcopy Assets dist\Assets


dotnet publish superAwesomeAmazingTourPlanner\superAwesomeAmazingTourPlanner.csproj -r win-x64 -c Release -o dist -p:PublishSingleFile=true --self-contained true

tar.exe -c -f dist.zip dist