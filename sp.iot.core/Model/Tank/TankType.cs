using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public enum TankType
    {
        CleanWater = 10,
        GrayWater = 20,
        BlackWater = 30,
        Propane = 40,
        Fuel = 50,
    }
}
