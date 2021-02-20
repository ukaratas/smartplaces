
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
            if (string.IsNullOrEmpty(value.ToString())) value = Guid.Empty.ToString();
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


        public static Gadget FindGadget(this Settings settings, Guid gadgetId)
        {
            foreach (var region in settings.Regions)
            {
                foreach (var section in region.Sections)
                {

                    foreach (var gadget in section.Gadgets)
                    {
                        if (gadget.Id == gadgetId)
                        {
                            gadget.Parent = section.Id;
                            return gadget;
                        }
                    }
                }
            }
            return null;
        }


        public static GadgetAction FindGadgetAction(this Settings settings, Guid gadgetActionId)
        {
            foreach (var region in settings.Regions)
            {
                foreach (var section in region.Sections)
                {

                    foreach (var gadget in section.Gadgets)
                    {
                        foreach (var gadgetAction in gadget.SourceActions)
                        {
                            if (gadgetAction.Id == gadgetActionId)
                            {
                                gadgetAction.Parent = gadget.Id;
                                return gadgetAction;
                            }
                        }
                    }
                }
            }
            return null;
        }


        public static GadgetDefinition FindGadgetDefinition(this Settings settings, Guid gadgetDefinitionId)
        {
            foreach (var gadgetDefinition in settings.GadgetDefinitions)
            {
                if (gadgetDefinition.Id == gadgetDefinitionId)
                {
                    return gadgetDefinition;
                }
            }
            return null;
        }
    }
}
