#!/bin/sh
rm -rf bin
cd src/CeContracts
nuget restore
msbuild CeContracts.sln /t:"Clean;Rebuild" /p:"Configuration=Release"

cd ../CePersistence
nuget restore
msbuild eventstore/eventstore.csproj /t:"Clean;Rebuild" /p:"Configuration=Release"
msbuild CeRepository/CeRepository.csproj /t:"Clean;Rebuild" /p:"Configuration=Release"

cd ../CeWeighting
nuget restore
msbuild Implementation/CeWeighting.csproj /t:"Clean;Rebuild" /p:"Configuration=Release"

cd ../CeDomain
nuget restore
msbuild Implementation/CeDomain.csproj /t:"Clean;Rebuild" /p:"Configuration=Release"

cd ../CeRestServerNancy
nuget restore
msbuild CeRestServerNancy/CeRestServerNancy.csproj /t:"Clean;Rebuild" /p:"Configuration=Release"