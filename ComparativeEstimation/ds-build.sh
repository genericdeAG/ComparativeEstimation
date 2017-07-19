#!/bin/sh
cd src/buildAll
nuget restore
msbuild buildAllNancy.sln /t:"Clean;Rebuild" /p:"Configuration=Release;OutputPath=../../../bin" /p:Platform="x86"