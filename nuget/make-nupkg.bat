call "\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\Tools\VsDevCmd.bat"
msbuild /p:Configuration=Release ..\src\Showpony\Showpony.csproj
..\tools\nuget\nuget.exe pack Showpony.1.0.1.0.nuspec

