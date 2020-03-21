using System;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public enum PositionType
    {
        NotSet = 0,
        Free = 1,
        TopRight = 1001,
        TopCenter = 1002,
        TopLeft = 1003,
        MiddleRight = 2001,
        MiddleCenter = 2002,
        MiddleLeft = 2003,
        BottomRight = 3001,
        BottomCenter = 3002,
        BottomLeft = 3003
    }
}
