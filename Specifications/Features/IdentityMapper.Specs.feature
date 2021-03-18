Feature: Timeseries mapping
    Put feature description here

    Background: Given tag mapping
        Given mappings between the following tags
            | Source   | From            | To                                   |
            | KChief   | MA80            | b9cd4c34-6d3b-4f74-9be9-f44b4fab2d4c |
            | Terasaki | 1:12            | 84acff21-e288-4111-af50-79e4224535a0 |
            | NMEA     | SpeedOverGround | 5c9d2bf0-7740-4551-9600-ce074398604e |
            | Modbus   | 3:40001         | 411e52ac-a912-43d8-a399-8b1595aeac02 |

    Scenario: Check if tag mapping exists
        When the following tags are requested
            | Source         | Tag     |
            | KChief         | MA80    |
            | Terasaki       | 1:12    |
            | Modbus         | 3:40001 |
            | Modbus         | sauron  |
            | Something tull | gandalf |
        Then the following tags will be mapped
            | Source   | Tag     | TimeSeries                           |
            | KChief   | MA80    | b9cd4c34-6d3b-4f74-9be9-f44b4fab2d4c |
            | Terasaki | 1:12    | 84acff21-e288-4111-af50-79e4224535a0 |
            | Modbus   | 3:40001 | 411e52ac-a912-43d8-a399-8b1595aeac02 |
        And the following tags will not be mapped
            | Source         | Tag     |
            | Something tull | gandalf |
            | Modbus         | sauron  |


