using System;

namespace sp.iot.core
{
    public enum GadgetTypeGroup
    {
        NotSet = 0,
        Button = 4000,
        Relay = 5000,
        Sensor = 9000,
    }

    public enum GadgetType
    {
        NotSet = 0,
        Valve = 3000,
        Switch = 4000,
        Relay = 5000,
        Current = 6000,
        Sensor = 9000,
        Other = 10000,
        LiquidFlowMeterYF06 = 2001,
        SwitchForLight = 4001,
        SwitchForDevice = 4002,
        RelayForLight = 5001,
        RelayForOutlet = 5002,
        RelayForDevice = 5003,
        SensorAirSHT30Heat = 9001,
        SensorAirSHT30Humidity = 9002,
        SensorGasMQ6 = 9003,
        SensorAirBME280Temprature = 9021,
        SensorAirBME280Humidity = 9022,
        SensorAirBME280Pressure = 9023,
        SensorLevelAnalog190Ohm = 9030,
        SensorLiquidFlowMeterYF06 = 9040,
        OtherBuzzer = 10001
    }
}
