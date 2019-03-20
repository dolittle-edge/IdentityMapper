# Identity Mapper

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