using System;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public enum PositionType
    {
        Free = 1,
        TopRight = 101,
        TopCenter = 102,
        TopLeft = 103,
        MiddleRight = 201,
        MiddleCenter = 202,
        MiddleLeft = 203,
        BottomRight = 301,
        BottomCenter = 302,
        BottomLeft = 303
    }
}
