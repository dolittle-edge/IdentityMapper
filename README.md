# IdentityMapper

[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=RaaLabs_IdentityMapper&metric=coverage)](https://sonarcloud.io/dashboard?id=RaaLabs_IdentityMapper)

This document describes the IdentityMapper module for RaaLabs Edge.

## What does it do?

IdentityMapper listens to messages received from *RaaLabs.Edge.Connectors* with the property `[InputName("events")]` and are producing the events `[OutputName("Translated")]`. The objective of this module is to map system-specific tag names to a Timeseries represented by a GUID.

## Configuration

The module is configured using a JSON file like the one below. If an `[InputName("events")]` contains both the *Source* and the *Tag* it will be mapped to the corresponding *Timeseries*

```json
{
  "KChief": {
    "K07": "c1f0c00e-3e36-43e3-8956-17572547d0e7",
    "K08": "7c26c02c-4b8d-4a70-aa09-fc295ae0bb7b"
  },
  "NMEA": {
    "HeadingTrue": "dbcb5edd-f93c-4f90-bf48-fc4eb4380a0d"
  }
}
```


## IoT Edge Deployment

### $edgeAgent

In your `deployment.json` file, you will need to add the module. For more details on modules in IoT Edge, go [here](https://docs.microsoft.com/en-us/azure/iot-edge/module-composition).

The module has persistent state and it is assuming that this is in the `data` folder relative to where the binary is running.
Since this is running in a containerized environment, the state is not persistent between runs. To get this state persistent, you'll
need to configure the deployment to mount a folder on the host into the data folder.

In your `deployment.json` file where you added the module, inside the `HostConfig` property, you should add the
volume binding.

```json
"Binds": [
    "<mount-path>:/app/data"
]
```

```json
{
    "modulesContent": {
        "$edgeAgent": {
            "properties.desired.modules.IdentityMapper": {
                "settings": {
                    "image": "<repo-name>/identitymapper:<tag>",
                    "createOptions": "{\"HostConfig\":{\"Binds\":[\"<mount-path>:/app/data\"]}}"
                },
                "type": "docker",
                "version": "1.0",
                "status": "running",
                "restartPolicy": "always"
            }
        }
    }
}
```

For production setup, the bind mount can be replaced by a docker volume.

### $edgeHub

The routes in edgeHub can be specified like the example below. All *RaaLabs.Edge.Connectors* send data into IdentityMapper. If the *Source* and the *Tag* exists, IdentityMapper will produce the *Translated* event. 

```json
{
    "$edgeHub": {
        "properties.desired.routes.<SomeConnector>IdentityMapper": "FROM /messages/modules/<SomeConnector>/outputs/* INTO BrokeredEndpoint(\"/modules/NewIdentityMapper/inputs/events\")",
        "properties.desired.routes.<SomeOtherConnector>IdentityMapper": "FROM /messages/modules/<SomeOtherConnector>/outputs/* INTO BrokeredEndpoint(\"/modules/NewIdentityMapper/inputs/events\")",
        "properties.desired.routes.IdentityMapperTo<SomeModule>": "FROM /messages/modules/IdentityMapper/outputs/Translated INTO BrokeredEndpoint(\"/modules/<SomeModule>/inputs/<inputevent>\")"
    }
}
```

