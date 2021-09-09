dotnet publish superAwesomeAmazingTourPlanner\superAwesomeAmazingTourPlanner.csproj -r win-x64 -c Release -o dist -p:PublishSingleFile=true --self-contained true

tar.exe -c -f dist.zip dist
