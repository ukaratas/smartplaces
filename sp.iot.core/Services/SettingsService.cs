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
        private readonly IGadgetService _gadgetService;

        public SettingsService(IConfiguration config, IDatabase database, IGadgetService gadgetService)
        {
            _config = config;
            _database = database;
            _gadgetService = gadgetService;
        }

        public Settings Get()
        {
            var settings = new Settings();


            SqliteDataReader gadgetDefinitionReader = _database.ExecuteReader(ConstantStrings.SqlQueries.GadgetDefinition.Get.All);
            while (gadgetDefinitionReader.Read())
            {
                var gadgetDefinition = _bindGadgetDefinitionData(gadgetDefinitionReader);
                settings.GadgetDefinitions.Add(gadgetDefinition);
            }

            SqliteDataReader regionReader = _database.ExecuteReader(ConstantStrings.SqlQueries.Region.Get.All);

            while (regionReader.Read())
            {
                var region = _bindRegionData(regionReader);
                settings.Regions.Add(region);

                SqliteDataReader rowReader = _database.ExecuteReader(
                   ConstantStrings.SqlQueries.RegionRows.Get.FilterByRegion,
                   new List<SqliteParameter> { new SqliteParameter("Region", region.Id) });

                while (rowReader.Read())
                {
                    var row = _bindRegionRowData(rowReader);
                    region.Rows.Add(row);
                }

                SqliteDataReader sectionReader = _database.ExecuteReader(
                    ConstantStrings.SqlQueries.Section.Get.FilterByRegion,
                    new List<SqliteParameter> { new SqliteParameter("Region", region.Id) });

                while (sectionReader.Read())
                {
                    var section = _bindSectionData(sectionReader);
                    region.Sections.Add(section);
                    section.Gadgets.AddRange(_gadgetService.GetBySection(section.Id, true));
                }
            }
            return settings;
        }

        public SaveResponse<Settings> Save(Settings request)
        {
            var returnValue = new SaveResponse<Settings>();


            request.GadgetDefinitions.ForEach(
                gadgetDefinition =>
                {

                    _database.SaveItem(
                                    gadgetDefinition.Id,
                                    ConstantStrings.SqlQueries.GadgetDefinition.Get.IdParam,
                                    ConstantStrings.SqlQueries.GadgetDefinition.Save.Insert,
                                    ConstantStrings.SqlQueries.GadgetDefinition.Save.UpdateWithId,
                                    new List<SaveItemProperty> {
                                                    new SaveItemProperty { Name= "Name", Value = gadgetDefinition.Name},
                                                    new SaveItemProperty { Name= "Type", Value = gadgetDefinition.Type},
                                                    new SaveItemProperty { Name= "Unit", Value = gadgetDefinition.Unit},
                                                    new SaveItemProperty { Name= "ReadScript", Value = gadgetDefinition.ReadScript},
                                                    new SaveItemProperty { Name= "WriteScript", Value = gadgetDefinition.WriteScript},

                                    },
                                    (log, actionType) => { returnValue.AddAction(string.Format("Gadget Definition '{0}' : {1}", gadgetDefinition.Name, log), actionType); }
                                    );

                });




            request.Regions.ForEach(
                region =>
                {
                    region.Rows.ForEach(row =>
                    {
                        _database.SaveItem(
                                      row.Id,
                                      ConstantStrings.SqlQueries.RegionRows.Get.IdParam,
                                      ConstantStrings.SqlQueries.RegionRows.Save.Insert,
                                      ConstantStrings.SqlQueries.RegionRows.Save.UpdateWithId,
                                      new List<SaveItemProperty> {
                                                    new SaveItemProperty { Name= "No", Value = row.No},
                                                    new SaveItemProperty { Name= "Percentage", Value = row.Percentage},

                                      },
                                      (log, actionType) => { returnValue.AddAction(string.Format("Percante '{0}' : {1}", row.No, log), actionType); }
                                      );

                    });

                    region.Sections.ForEach(
                        section =>
                        {
                            section.Gadgets.ForEach(
                                gadget =>
                                {

                                    gadget.Actions.ForEach(
                                        action =>
                                        {
                                            _database.SaveItem(
                                                action.Id,
                                                ConstantStrings.SqlQueries.GadgetAction.Get.IdParam,
                                                ConstantStrings.SqlQueries.GadgetAction.Save.Insert,
                                                ConstantStrings.SqlQueries.GadgetAction.Save.UpdateWithId,
                                                new List<SaveItemProperty> {
                                                    new SaveItemProperty { Name= "Order", Value = action.Order},
                                                    new SaveItemProperty { Name= "SourceGadget", Value = gadget.Id},
                                                    new SaveItemProperty { Name= "TargetComplexValue", Value = action.TargetComplexValue},
                                                    new SaveItemProperty { Name= "TargetGadget", Value = action.TargetGadget},
                                                    new SaveItemProperty { Name= "TargetValue", Value = action.TargetValue },
                                                    new SaveItemProperty { Name= "CanExecute", Value = action.CanExecute },
                                                    },
                                                    (log, actionType) => { returnValue.AddAction(string.Format("Gadget Action '{0}' : {1}", action.Order, log), actionType); }
                                                    );
                                        }
                                    );


                                    _database.SaveItem(
                                            gadget.Id,
                                            ConstantStrings.SqlQueries.Gadget.Get.IdParam,
                                            ConstantStrings.SqlQueries.Gadget.Save.Insert,
                                            ConstantStrings.SqlQueries.Gadget.Save.UpdateWithId,
                                            new List<SaveItemProperty> {
                                                    new SaveItemProperty { Name= "Name", Value = gadget.Name},
                                                    new SaveItemProperty { Name= "Configuration", Value = gadget.Configuration},
                                                    new SaveItemProperty { Name= "Value", Value = gadget.Value },
                                                    new SaveItemProperty { Name= "ComplexValue", Value = gadget.ComplexValue},
                                                    new SaveItemProperty { Name= "Status", Value = gadget.Status},
                                                    new SaveItemProperty { Name= "Section", Value = section.Id },
                                                    new SaveItemProperty { Name= "SectionPosition", Value = gadget.SectionPosition },
                                                    new SaveItemProperty { Name= "Definition", Value = gadget.Definition},

                                            },
                                            (log, actionType) => { returnValue.AddAction(string.Format("Gadget '{0}' : {1}", gadget.Name, log), actionType); }
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
                                                    new SaveItemProperty { Name= "Type", Value = section.Type},
                                                    new SaveItemProperty { Name= "Row", Value = section.Row},
                                                    new SaveItemProperty { Name= "Column", Value = section.Column},

                                       },
                                       (log, actionType) => { returnValue.AddAction(string.Format("Section '{0}' : {1}", section.Name, log), actionType); }
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
                                                    new SaveItemProperty { Name= "BackgroundImage", Value = region.BackgroundImage},
                                                    new SaveItemProperty { Name= "AspectRatio", Value = region.AspectRatio},

                                     },
                                     (log, actionType) => { returnValue.AddAction(string.Format("Region '{0}' : {1}", region.Name, log), actionType); }
                                     );
                });

            returnValue.Item = Get();

            return returnValue;
        }
        public SaveResponse<GadgetDefinition> DeleteGadgetDefinition(Guid gadgetDefinitionId)
        {
            var returnValue = new SaveResponse<GadgetDefinition>();

            //1- Check Existance
            returnValue.AddAction("Checking any dependend gadget existance", SaveActionType.Information);

            SqliteDataReader reader = _database.ExecuteReader(
                ConstantStrings.SqlQueries.Gadget.Get.FilterByDefinition,
                new List<SqliteParameter> { new SqliteParameter("DefinitionId", gadgetDefinitionId) }
                );

            if (reader.Read())
            {
                returnValue.AddAction("Gadget definition has gadgets", SaveActionType.Warning);
                return returnValue;
            }
            else
            {
                returnValue.AddAction("Gadget definition has not dependent gadgets", SaveActionType.Information);
            }

            //3- Delete


            var deleteResult = _database.ExecuteNonQuery(
                ConstantStrings.SqlQueries.GadgetDefinition.Delete.DeleteWithId,
                new List<SqliteParameter> { new SqliteParameter("Id", gadgetDefinitionId) }
                );

            if (deleteResult > 0)
            {
                returnValue.AddAction("Gadget definition has deleted successfully", SaveActionType.Success);
                return returnValue;
            }
            else
            {
                returnValue.AddAction("Gadget definition  has not deleted !", SaveActionType.Error);
            }

            return returnValue;
        }
        public SaveResponse<Gadget> DeleteGadget(Guid gadgetId)
        {
            var returnValue = new SaveResponse<Gadget>();

            //1- Check Action Existance
            returnValue.AddAction("Checking any dependend source gadget actions", SaveActionType.Information);

            SqliteDataReader reader = _database.ExecuteReader(
                ConstantStrings.SqlQueries.GadgetAction.Get.BySourceGadget,
                new List<SqliteParameter> { new SqliteParameter("SourceGadget", gadgetId) }
                );

            if (reader.Read())
            {
                returnValue.AddAction("Gadget definition has gadgets action as a source", SaveActionType.Warning);
                return returnValue;
            }
            else
            {
                returnValue.AddAction("Gadget definition has not dependent gadget action as a source", SaveActionType.Information);
            }

            reader = _database.ExecuteReader(
                ConstantStrings.SqlQueries.GadgetAction.Get.ByTargetGadget,
                new List<SqliteParameter> { new SqliteParameter("TargetGadget", gadgetId) }
                );

            if (reader.Read())
            {
                returnValue.AddAction("Gadget definition has gadgets action as a target", SaveActionType.Warning);
                return returnValue;
            }
            else
            {
                returnValue.AddAction("Gadget definition has not dependent gadget action as a target", SaveActionType.Information);
            }

            //3- Delete


            var deleteResult = _database.ExecuteNonQuery(
                ConstantStrings.SqlQueries.Gadget.Delete.DeleteWithId,
                new List<SqliteParameter> { new SqliteParameter("Id", gadgetId) }
                );

            if (deleteResult > 0)
            {
                returnValue.AddAction("Gadget has deleted successfully", SaveActionType.Success);
                return returnValue;
            }
            else
            {
                returnValue.AddAction("Gadget has not deleted !", SaveActionType.Error);
            }

            return returnValue;
        }
        public SaveResponse<Region> DeleteRegion(Guid regionId)
        {
            var returnValue = new SaveResponse<Region>();

            //1- Check Existance
            returnValue.AddAction("Checking any dependend section existance", SaveActionType.Information);

            Region region = null;
            SqliteDataReader reader = _database.ExecuteReader(
                ConstantStrings.SqlQueries.Region.Get.IdParam,
                new List<SqliteParameter> { new SqliteParameter("Id", regionId) }
                );

            if (reader.Read())
            {
                region = _bindRegionData(reader);
                returnValue.Item = region;
                returnValue.AddAction("Region is exist", SaveActionType.Information);
            }
            else
            {
                returnValue.AddAction("Region is NOT exist", SaveActionType.Error);
                return returnValue;
            }

            //2- Check Dependency - Any Section

            reader = _database.ExecuteReader(
                ConstantStrings.SqlQueries.Section.Get.FilterByRegion,
                new List<SqliteParameter> { new SqliteParameter("Region", regionId) }
                );

            if (reader.Read())
            {
                returnValue.AddAction("Region has sections", SaveActionType.Warning);
                return returnValue;
            }
            else
            {
                returnValue.AddAction("Region has not dependent sections", SaveActionType.Information);
            }

            //3- Delete


            var deleteResult = _database.ExecuteNonQuery(
                ConstantStrings.SqlQueries.Region.Delete.DeleteWithId,
                new List<SqliteParameter> { new SqliteParameter("Id", regionId) }
                );

            if (deleteResult > 0)
            {
                returnValue.AddAction("Region has deleted successfully", SaveActionType.Success);
                return returnValue;
            }
            else
            {
                returnValue.AddAction("Region has not deleted !", SaveActionType.Error);
            }

            return returnValue;
        }
        public SaveResponse<Section> DeleteSection(Guid sectionId)
        {

            var returnValue = new SaveResponse<Section>();

            //1- Check Existance

            returnValue.AddAction("Checking section existance");

            Section section = null;
            SqliteDataReader reader = _database.ExecuteReader(
                ConstantStrings.SqlQueries.Section.Get.IdParam,
                new List<SqliteParameter> { new SqliteParameter("Id", sectionId) }
                );

            if (reader.Read())
            {
                section = _bindSectionData(reader);
                returnValue.Item = section;
                returnValue.AddAction("Section is exist", SaveActionType.Information);
            }
            else
            {
                returnValue.AddAction("Section is NOT exist", SaveActionType.Error);
                return returnValue;
            }

            //2- Check Dependency - Any Gadget

            returnValue.AddAction("Checking dependent gadgets");
            List<Gadget> gadgets = _gadgetService.GetBySection(sectionId, false);
            if (gadgets.Count > 0)
            {
                returnValue.AddAction("Section has gadgets", SaveActionType.Warning);
                return returnValue;
            }
            else
            {
                returnValue.AddAction("Section has not dependent gadgets", SaveActionType.Information);
            }

            //3- Delete


            var deleteResult = _database.ExecuteNonQuery(
                ConstantStrings.SqlQueries.Section.Delete.DeleteWithId,
                new List<SqliteParameter> { new SqliteParameter("Id", sectionId) }
                );

            if (deleteResult > 0)
            {
                returnValue.AddAction("Section has deleted successfully", SaveActionType.Success);
                return returnValue;
            }
            else
            {
                returnValue.AddAction("Section has not deleted !", SaveActionType.Error);
            }

            return returnValue;
        }
        private Region _bindRegionData(SqliteDataReader reader)
        {
            Region returnValue = new Region
            {
                Id = reader.GetValueAsGuid("Id"),
                Name = reader.GetValue(reader.GetOrdinal("Name")).ToString(),
                Type = (RegionType)(int)(long)reader.GetValue(reader.GetOrdinal("Type")),
                AspectRatio = (double)reader.GetValue(reader.GetOrdinal("AspectRatio")),
                BackgroundImage = reader.GetValue(reader.GetOrdinal("BackgroundImage")).ToString(),
            };
            return returnValue;
        }
        private RegionRow _bindRegionRowData(SqliteDataReader reader)
        {
            RegionRow returnValue = new RegionRow
            {
                Id = reader.GetValueAsGuid("Id"),
                No = (int)(long)reader.GetValue(reader.GetOrdinal("No")),
                Percentage = (int)(long)reader.GetValue(reader.GetOrdinal("Percentage")),
            };
            return returnValue;
        }

        private Section _bindSectionData(SqliteDataReader reader)
        {
            Section returnValue = new Section
            {
                Id = reader.GetValueAsGuid("Id"),
                Name = reader.GetValue(reader.GetOrdinal("Name")).ToString(),
                Type = (SectionType)(int)(long)reader.GetValue(reader.GetOrdinal("Type")),
                Row = (long)reader.GetValue(reader.GetOrdinal("Row")),
                Column = (long)reader.GetValue(reader.GetOrdinal("Column")),
            };
            return returnValue;
        }


        private GadgetDefinition _bindGadgetDefinitionData(SqliteDataReader reader)
        {
            var returnValue = new GadgetDefinition
            {
                Id = reader.GetValueAsGuid("Id"),
                Name = reader.GetValue(reader.GetOrdinal("Name")).ToString(),
                Type = (GadgetType)(int)(long)reader.GetValue(reader.GetOrdinal("Type")),
                Unit = (UnitType)(int)(long)reader.GetValue(reader.GetOrdinal("Unit")),
                ReadScript = reader.GetValue(reader.GetOrdinal("ReadScript")).ToString(),
                WriteScript = reader.GetValue(reader.GetOrdinal("WriteScript")).ToString(),
            };
            return returnValue;
        }
    }
}