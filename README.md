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