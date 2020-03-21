using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;


namespace sp.iot.core
{
    public class SettingsService : ISettingsService
    {
        private readonly IConfiguration _config;
        private readonly IDatabase _database;

        public SettingsService(IConfiguration config, IDatabase database)
        {
            _config = config;
            _database = database;
        }

        public Settings Get()
        {
            var settings = new Settings();

            SqliteDataReader regionReader = _database.ExecuteReader(ConstantStrings.SqlQueries.Region.Get.All);

            while (regionReader.Read())
            {
                var region = _bindRegionData(regionReader);
                settings.Regions.Add(region);

                SqliteDataReader sectionReader = _database.ExecuteReader(
                    ConstantStrings.SqlQueries.Section.Get.FilterByRegion,
                    new List<SqliteParameter> { new SqliteParameter("Region", region.Id) });

                while (sectionReader.Read())
                {
                    var section = _bindSectionData(sectionReader);
                    region.Sections.Add(section);

                    SqliteDataReader gadgetReader = _database.ExecuteReader(
                        ConstantStrings.SqlQueries.Gadget.Get.FilterBySection,
                        new List<SqliteParameter> { new SqliteParameter("Section", section.Id) });

                    while (gadgetReader.Read())
                    {
                        var gadget = _bindGadgetData(gadgetReader);
                        section.Gadgets.Add(gadget);
                    }
                }
            }
            return settings;
        }

        public SaveResponse<Settings> Save(Settings request)
        {
            var returnValue = new SaveResponse<Settings>();
            request.Regions.ForEach(
                region =>
                {
                    region.Sections.ForEach(
                        section =>
                        {
                            section.Gadgets.ForEach(
                                gadget =>
                                {
                                    _database.SaveItem(
                                            gadget.Id,
                                            ConstantStrings.SqlQueries.Gadget.Get.IdParam,
                                            ConstantStrings.SqlQueries.Gadget.Save.Insert,
                                            ConstantStrings.SqlQueries.Gadget.Save.UpdateWithId,
                                            new List<SaveItemProperty> {
                                                    new SaveItemProperty { Name= "Name", Value = gadget.Name},
                                                    new SaveItemProperty { Name= "Type", Value = gadget.Type},
                                                    new SaveItemProperty { Name= "Port", Value = gadget.Port},
                                                    new SaveItemProperty { Name= "Value", Value = gadget.Value },
                                                    new SaveItemProperty { Name= "ValueUnit", Value = gadget.ValueUnit },
                                                    new SaveItemProperty { Name= "ValueToTargetRatio", Value = gadget.ValueToTargetRatio },
                                                    new SaveItemProperty { Name= "ValueToTargetUnit", Value = gadget.ValueToTargetUnit },
                                                    new SaveItemProperty { Name= "Section", Value = section.Id },
                                                    new SaveItemProperty { Name= "SectionPosition", Value = gadget.SectionPosition },
                                                    new SaveItemProperty { Name= "ComplexValue", Value = gadget.ComplexValue},
                                                    new SaveItemProperty { Name= "AttachedTo", Value = gadget.AttachedTo},
                                                    new SaveItemProperty { Name= "Status", Value = gadget.Status},
                                            },
                                            (log) => { returnValue.AddAction(string.Format("Gadget '{0}' : {1}", gadget.Name, log)); }
                                            );

                                });

                            _database.SaveItem(
                                       section.Id,
                                       ConstantStrings.SqlQueries.Section.Get.IdParam,
                                       ConstantStrings.SqlQueries.Section.Save.Insert,
                                       ConstantStrings.SqlQueries.Section.Save.UpdateWithId,
                                       new List<SaveItemProperty> {
                                                    new SaveItemProperty { Name= "Name", Value = section.Name},
                                                    new SaveItemProperty { Name= "Region", Value = region.Id},
                                                    new SaveItemProperty { Name= "RightSection", Value = section.RightSection},
                                                    new SaveItemProperty { Name= "LeftSection", Value = section.LeftSection},
                                                    new SaveItemProperty { Name= "TopSection", Value = section.TopSection},
                                                    new SaveItemProperty { Name= "BottomSection", Value = section.BottomSection},
                                       },
                                       (log) => { returnValue.AddAction(string.Format("Section '{0}' : {1}", section.Name, log)); }
                                       );
                        });

                    _database.SaveItem(
                                     region.Id,
                                     ConstantStrings.SqlQueries.Region.Get.IdParam,
                                     ConstantStrings.SqlQueries.Region.Save.Insert,
                                     ConstantStrings.SqlQueries.Region.Save.UpdateWithId,
                                     new List<SaveItemProperty> {
                                                    new SaveItemProperty { Name= "Name", Value = region.Name},
                                                    new SaveItemProperty { Name= "Type", Value = region.Type},
                                     },
                                     (log) => { returnValue.AddAction(string.Format("Region '{0}' : {1}", region.Name, log)); }
                                     );
                });

            _database.Close();
            return returnValue;
        }


        private Region _bindRegionData(SqliteDataReader reader)
        {
            Region returnValue = new Region
            {
                Id = reader.GetValueAsGuid("Id"),
                Name = reader.GetValue(reader.GetOrdinal("Name")).ToString(),
                Type = (RegionType)(int)(long)reader.GetValue(reader.GetOrdinal("Type")),
            };
            return returnValue;
        }


        private Section _bindSectionData(SqliteDataReader reader)
        {
            Section returnValue = new Section
            {
                Id = reader.GetValueAsGuid("Id"),
                Name = reader.GetValue(reader.GetOrdinal("Name")).ToString(),
                RightSection = reader.GetValueAsGuid("RightSection"),
                LeftSection = reader.GetValueAsGuid("LeftSection"),
                TopSection = reader.GetValueAsGuid("TopSection"),
                BottomSection = reader.GetValueAsGuid("BottomSection"),
            };
            return returnValue;
        }

        private Gadget _bindGadgetData(SqliteDataReader reader)
        {
            Gadget returnValue = new Gadget
            {
                Id = reader.GetValueAsGuid("Id"),
                Name = reader.GetValue(reader.GetOrdinal("Name")).ToString(),
                Type = (GadgetType)(int)(long)reader.GetValue(reader.GetOrdinal("Type")),
                Port = reader.GetValue(reader.GetOrdinal("Port")).ToString(),
                Status = (GadgetStatus)(int)(long)reader.GetValue(reader.GetOrdinal("Status")),
                Value = (double)reader.GetValue(reader.GetOrdinal("Value")),
                ValueUnit = (UnitType)(int)(long)reader.GetValue(reader.GetOrdinal("ValueUnit")),
                ValueToTargetRatio = (double)reader.GetValue(reader.GetOrdinal("ValueToTargetRatio")),
                ValueToTargetUnit = (UnitType)(int)(long)reader.GetValue(reader.GetOrdinal("ValueToTargetUnit")),
                ComplexValue = reader.GetValue(reader.GetOrdinal("ComplexValue")).ToString(),
                SectionPosition = (PositionType)(int)(long)reader.GetValue(reader.GetOrdinal("SectionPosition")),
                AttachedTo = reader.GetValueAsGuid("AttachedTo"),
            };
            return returnValue;
        }




    }
}