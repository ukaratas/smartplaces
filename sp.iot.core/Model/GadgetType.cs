using System;

namespace sp.iot.core
{
    public enum GadgetTypeGroup
    {
        NotSet = 0,
        Level = 1000,
        LiquidFlowMeter = 2000,
        Valve = 3000,
        Switch = 4000,
        Relay = 5000,
        Current = 6000,

        Sensor = 9000
    }

    public enum GadgetType
    {
        NotSet = 0,
        LevelAnalog190Ohm = 1001,
        LevelDigitalGeneric = 1002,
        LiquidFlowMeterYF06 = 2001,
        ValveNormallyClosed = 3001,
        SwitchPush = 4001,
        RelayForLight = 5001,
        RelayForSocket = 5002,
        RelayForDevice = 5003,
        SensorAirDTH22Heat = 9001,
        SensorAirDTH22Humidity = 9002,
    }
}
