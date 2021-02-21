
using Microsoft.Extensions.DependencyInjection;

namespace sp.iot.core
{
    public static class ConstantStrings
    {
        public static class SqlQueries
        {
            public static class GadgetDefinition
            {
                public static class Get
                {
                    public const string All = "SELECT * FROM GadgetDefinitions";
                    public const string IdParam = "SELECT * FROM GadgetDefinitions WHERE Id = @Id";
                }

                public static class Save
                {
                    public const string UpdateWithId = "UPDATE GadgetDefinitions SET Name = @Name, Type = @Type, ReadScript = @ReadScript, WriteScript = @WriteScript WHERE Id = @Id";
                    public const string Insert = "INSERT INTO GadgetDefinitions (Id, Name, Type, ReadScript, WriteScript)VALUES(@Id,@Name,@Type, @ReadScript, @WriteScript)";
                }

                public static class Delete
                {
                    public const string DeleteWithId = "DELETE FROM GadgetDefinitions WHERE Id = @Id";
                }
            }


            public static class Region
            {
                public static class Get
                {
                    public const string All = "SELECT * FROM Regions";
                    public const string IdParam = "SELECT * FROM Regions WHERE Id = @Id";
                }

                public static class Save
                {
                    public const string UpdateWithId = "UPDATE Regions SET Name = @Name, Type = @Type, BackgroundImage= @BackgroundImage, AspectRatio = @AspectRatio WHERE Id = @Id";
                    public const string Insert = "INSERT INTO Regions (Id,Name,Type, BackgroundImage, AspectRatio)VALUES(@Id,@Name,@Type, @BackgroundImage, @AspectRatio)";
                }

                public static class Delete
                {
                    public const string DeleteWithId = "DELETE FROM Regions WHERE Id = @Id";
                }
            }


            public static class RegionRows
            {
                public static class Get
                {
                    public const string All = "SELECT * FROM RegionRows";
                    public const string IdParam = "SELECT * FROM RegionRows WHERE Id = @Id";
                    public const string FilterByRegion = "SELECT * FROM RegionRows WHERE Region = @Region";
                }

                public static class Save
                {
                    public const string UpdateWithId = "UPDATE RegionRows SET No = @No, Percentage = @Percentage WHERE Id = @Id";
                    public const string Insert = "INSERT INTO RegionRows (Id, No, Percentage)VALUES(@Id, @No, @Percentage)";


                }


            }


            public static class Section
            {
                public static class Get
                {
                    public const string FilterByRegion = "SELECT * FROM Sections WHERE Region = @Region";
                    public const string IdParam = "SELECT * FROM Sections WHERE Id = @Id";
                }

                public static class Save
                {
                    public const string UpdateWithId = "UPDATE Sections SET Name = @Name, Region = @Region, Type = @Type, Row = @Row, Column = @Column WHERE Id = @Id";
                    public const string Insert = "INSERT INTO Sections (Id,Name,Region,Type,Row,Column)VALUES(@Id,@Name,@Region,@Type,@Row,@Column)";
                }

                public static class Delete
                {
                    public const string DeleteWithId = "DELETE FROM Sections WHERE Id = @Id";
                }
            }


            public static class Gadget
            {
                public static class Get
                {
                    public const string All = "SELECT * FROM Gadgets";
                    public const string AllIncludeRegion = "SELECT Gadgets.*, Sections.Region FROM Gadgets LEFT JOIN Sections ON Section = Sections.Id";
                    public const string FilterBySection = "SELECT * FROM Gadgets WHERE Section = @Section";
                    public const string FilterByDefinition = "SELECT * FROM Gadgets WHERE Definition = @Definition";
                    public const string IdParam = "SELECT * FROM Gadgets WHERE Id = @Id";
                }

                public static class Save
                {
                    public const string UpdateWithId = "UPDATE Gadgets SET Name = @Name,Configuration = @Configuration,Status = @Status,Value = @Value, ComplexValue = @ComplexValue,Section = @Section,SectionPosition = @SectionPosition, Definition = @Definition, ReadFrequency = @ReadFrequency WHERE Id = @Id";
                    public const string Insert = "INSERT INTO Gadgets (Id,Name,Configuration,Status,Value,ComplexValue,Section,SectionPosition,Definition, ReadFrequency) VALUES (@Id,@Name,@Configuration,@Status,@Value,@ComplexValue,@Section,@SectionPosition,@Definition,@ReadFrequency)";
                }

                public static class Update
                {
                    public const string UpdateValue = "UPDATE Gadgets SET Value = @Value, ComplexValue = @ComplexValue WHERE Id = @Id";
                }

                public static class Delete
                {
                    public const string DeleteWithId = "DELETE FROM Gadgets WHERE Id = @Id";
                }
            }

            public static class GadgetAction
            {
                public static class Get
                {
                    public const string IdParam = "SELECT * FROM GadgetActions WHERE Id = @Id";
                    public const string BySourceGadget = "SELECT * FROM GadgetActions WHERE SourceGadget = @SourceGadget";
                    public const string ByTargetGadget = "SELECT * FROM GadgetActions WHERE TargetGadget = @TargetGadget";
                }

                public static class Save
                {
                    public const string UpdateWithId = "UPDATE GadgetActions SET 'Order' = @Order, CanExecute = @CanExecute, SourceGadget = @SourceGadget, TargetGadget = @TargetGadget, Execute = @Execute WHERE Id = @Id";
                    public const string Insert = "INSERT INTO GadgetActions (Id,'Order',SourceGadget,CanExecute,TargetGadget,Execute) VALUES (@Id, @Order,@SourceGadget,@CanExecute,@TargetGadget,@Execute)";
                }

            }
        }
    }
}