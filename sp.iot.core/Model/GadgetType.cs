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
        Current = 5000,
    }

    public enum GadgetType
    {
        NotSet = 0,
        LevelAnalog190Ohm = 1001,
        LevelDigitalGeneric = 1002,
        LiquidFlowMeterYF06 = 2001,
        ValveNormallyClosed = 3001,
    }


}
