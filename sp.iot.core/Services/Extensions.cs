
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
            services.AddScoped<IGadgetService, GadgetService>();

        }

        public static Guid GetValueAsGuid(this SqliteDataReader reader, string fieldName)
        {
            var value = reader.GetValue(reader.GetOrdinal(fieldName));
            return Guid.Parse(value.ToString());
        }
    }
}
