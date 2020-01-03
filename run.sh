#!/bin/bash
docker build -f ./Source/Dockerfile -t raalabs/timeseries-identitymapper . --build-arg CONFIGURATION="Debug"
iotedgehubdev start -d deployment.json -v