
using System;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;

namespace sp.iot.core
{
    public static class Extensions
    {

        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IDatabase, Database>();
            services.AddScoped<ISettingsService, SettingsService>();
        }

        /*
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

      

        public static object GetDBChangeValue(this GadgetType? newValue, object oldValue)
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
*/
        public static Guid GetValueAsGuid(this SqliteDataReader reader, string fieldName)
        {
            var value = reader.GetValue(reader.GetOrdinal(fieldName));

            if (value == null || value == DBNull.Value)
                return Guid.Empty;
            else
            {
                return Guid.Parse(value.ToString());
            }
        }

        /*

         public static double GetValueAsDouble(this SqliteDataReader reader, string fieldName)
        {
            var value = reader.GetValue(reader.GetOrdinal(fieldName));

            if (value == null || value == DBNull.Value)
                return 0;
            else
            {
                return (double)value;
            }
        }
        */
    }
}