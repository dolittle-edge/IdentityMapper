# Identity Mapper

![CI/CD](https://github.com/RaaLabs/IdentityMapper/workflows/.NET%20Core/badge.svg)
[![codecov](https://codecov.io/gh/Raalabs/IdentityMapper.Common/branch/master/graph/badge.svg)](https://codecov.io/gh/Raalabs/IdentityMapper)


## Cloning

This repository has sub modules, clone it with:

```shell
$ git clone --recursive <repository url>
```

If you've already cloned it, you can get the submodules by doing the following:

```shell
$ git submodule update --init --recursive
```

## Building

All the build things are from a submodule.
To build, run one of the following:

Windows:

```shell
$ Build\build.cmd
```

Linux / macOS

```shell
$ Build\build.sh
```

## Getting started

This solution is built on top of [Azure IoT Edge](https://github.com/Azure/iotedge), and to be able to work locally and run it locally, you will need the development
environment - read more about that [here](https://docs.microsoft.com/en-us/azure/iot-edge/development-environment).
It mentions the use of the [iotedgedev](https://github.com/Azure/iotedgedev) tool.

### VSCode

If you are using VSCode or similar text editor, just open up the folder from the root. This solution uses a [sub-module](https://github.com/dolittle-tools/DotNET.Build) (as described above).
It comes with a few things that makes development a little bit easier, a set of VSCode tasks as described [here](https://github.com/dolittle-tools/DotNET.Build#visual-studio-code-tasks).

In addition to this there is a couple of Debug launch settings set up as well to enable debugging directly.

### Visual Studio 201x

Open up the [.sln](./IdentityMapper.sln) file at the root of the project.

## Deploying

### Module

In your `deployment.json` file, you will need to add the module. For more details on modules in IoT Edge, go [here](https://docs.microsoft.com/en-us/azure/iot-edge/module-composition).

```json
"modules": {
    "RaaLabs.TimeSeries.IdentityMapper": {
    "version": "1.0",
    "type": "docker",
    "status": "running",
    "restartPolicy": "always",
    "settings": {
        "image": "dolittle/timeseries-identitymapper",
        "createOptions": {
        "HostConfig": {}
    }
}
```

### State

The module depends has persistent state and it is assuming that this is in the `data` folder relative to where the binary is running.
Since this is running in a containerized environment, the state is not persistent between runs. To get this state persistent, you'll
need to configure the deployment to mount a folder on the host into the data folder.

In your `deployment.json` file where you added the module, inside the `HostConfig` property, you should add the
volume binding.

```json
"Binds": [
    "/etc/RaaLabs.TimeSeries/identitymapper:/app/data"
]
```

This should result in something like:

```json
"modules": {
    "RaaLabs.TimeSeries.IdentityMapper": {
    "version": "1.0",
    "type": "docker",
    "status": "running",
    "restartPolicy": "always",
    "settings": {
        "image": "shipos/timeseries-identitymapper",
        "createOptions": {
        "HostConfig": {
            "Binds": [
                "/etc/raalabs.timeseries/identitymapper:/app/data"
            ]
        }
    }
}
```