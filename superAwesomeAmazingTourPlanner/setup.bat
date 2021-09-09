:: arg 1 is OutputDir, arg 2 is SolutionDir
echo doing stuff in %1

if not exist %1LocalStorage  			mkdir %1LocalStorage
if not exist %1LocalStorage\Images  	mkdir %1LocalStorage\Images
if not exist %1LocalStorage\Reports  	mkdir %1LocalStorage\Reports
if not exist %1LocalStorage\Exports  	mkdir %1LocalStorage\Exports
if not exist %1Assets  					mkdir %1Assets

if exist %2Assets ( xcopy /y %2Assets %1Assets ) else ( xcopy /y Assets %1Assets )
