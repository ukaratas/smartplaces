
using System;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;

namespace sp.iot.core
{
    public static class Extensions
    {

        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<IDatabase, Database>();
            services.AddSingleton<IScriptingService, ScriptingService>();

            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IGadgetService, GadgetService>();
            services.AddScoped<IGadgetActionService, GadgetActionService>();


            services.AddTransient<IGadgetEngine, LevelAnalog190Ohm>();
        }

        public static Guid GetValueAsGuid(this SqliteDataReader reader, string fieldName)
        {
            var value = reader.GetValue(reader.GetOrdinal(fieldName));
            return Guid.Parse(value.ToString());
        }

        public static Section FindSection(this Settings settings, Guid sectionId)
        {
            foreach (var region in settings.Regions)
            {
                foreach (var section in region.Sections)
                {
                    if (section.Id == sectionId) 
                    {
                        section.Parent = region.Id;
                        return section;
                    }
                    
                }
            }
            return null;
        }


    }
}
