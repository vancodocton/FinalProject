ls -include TestResults -recurse | rm -recurse &; dotnet clean &;
dotnet restore
dotnet build . --no-restore --configuration Release
dotnet test . --configuration Release --no-build --settings ci.runsettings --verbosity normal