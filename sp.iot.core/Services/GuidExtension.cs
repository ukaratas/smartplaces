
using System;
using Microsoft.Extensions.DependencyInjection;

namespace sp.iot.core
{
    public static class GuidExtension
    {
        public static object ToDBString(this Guid? value)
        {
            if (value == null || value == Guid.Empty)
            {
                return DBNull.Value;
            }
            else
            {
                return value.ToString();
            }

        }


        public static object GetDBChangeValue(this Guid? newValue, object oldValue)
        {
            if (newValue == null)
            {

                return (oldValue as Guid?).ToDBString();
            }
            else
            {
                return newValue.ToString();
            }
        }

        public static object GetDBChangeValue(this TankType? newValue, object oldValue)
        {
            if (newValue == null || newValue == 0)
            {
                return oldValue;
            }
            else
            {
                return newValue;
            }

        }

        public static object GetDBChangeValue(this UnitType? newValue, object oldValue)
        {
            if (newValue == null || newValue == 0)
            {
                return oldValue;
            }
            else
            {
                return newValue;
            }

        }
    }
}