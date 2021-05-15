Feature: IdentityMapperHandler

    Background: 
        Given mappings between the following tags
            | Source   | From            | To                                   |
            | KChief   | MA80            | b9cd4c34-6d3b-4f74-9be9-f44b4fab2d4c |
            | Terasaki | 1:12            | 84acff21-e288-4111-af50-79e4224535a0 |
            | NMEA     | SpeedOverGround | 5c9d2bf0-7740-4551-9600-ce074398604e |
            | Modbus   | 3:40001         | 411e52ac-a912-43d8-a399-8b1595aeac02 |
        And a handler of type IdentityMapperHandler

    Scenario: Incoming event to handler
        When the following events of type EdgeHubDataPointReceived is produced
            | Source         | Tag     | Value   | Timestamp     |
            | KChief         | MA80    | 12.1    | 1616400494883 |
            | Terasaki       | 1:12    | 12.1    | 1616400494882 |
            | Modbus         | 3:40001 | 11232.1 | 1616400494881 |
            | Modbus         | sauron  | 12      | 1616400494885 |
            | Something tull | gandalf | 12.1    | 1616400494883 |

        Then the following events of type EdgeHubDataPointRemapped is produced
            | TimeSeries                           | Value   | Timestamp     |
            | b9cd4c34-6d3b-4f74-9be9-f44b4fab2d4c | 12.1    | 1616400494883 |
            | 84acff21-e288-4111-af50-79e4224535a0 | 12.1    | 1616400494882 |
            | 411e52ac-a912-43d8-a399-8b1595aeac02 | 11232.1 | 1616400494881 |