#!/bin/bash
dotnet build

rm -r Releases
mkdir Releases

dotnet publish --runtime linux-x64 --configuration Release /p:PublishSingleFile=true /p:PublishTrimmed=true

cp bin/Release/net5.0/linux-x64/publish/SetAlacrittyFontSize Releases
