
using Microsoft.Extensions.DependencyInjection;

namespace sp.iot.core
{
    public static class ConstantStrings
    {
        public static class SqlQueries
        {

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
                    public const string UpdateWithId = "UPDATE Sections SET Name = @Name, Region = @Region, Row = @Row, Column = @Column WHERE Id = @Id";
                    public const string Insert = "INSERT INTO Sections (Id,Name,Region,Row,Column)VALUES(@Id,@Name,@Region,@Row,@Column)";
                }
            }


            public static class Gadget
            {
                public static class Get
                {
                    public const string All = "SELECT * FROM Gadgets";
                    public const string AllIncludeRegion = "SELECT Gadgets.*, Sections.Region FROM Gadgets LEFT JOIN Sections ON Section = Sections.Id";
                    public const string FilterBySection = "SELECT * FROM Gadgets WHERE Section = @Section";
                    public const string IdParam = "SELECT * FROM Gadgets WHERE Id = @Id";
                }

                public static class Save
                {
                    public const string UpdateWithId = "UPDATE Gadgets SET Name = @Name,Type = @Type,TypeGroup = @TypeGroup,Port = @Port,Status = @Status,Value = @Value,ValueUnit = @ValueUnit,ValueToTargetRatio = @ValueToTargetRatio,ValueToTargetUnit = @ValueToTargetUnit,ComplexValue = @ComplexValue,Section = @Section,SectionPosition = @SectionPosition,AttachedTo = @AttachedTo WHERE Id = @Id";
                    public const string Insert = "INSERT INTO Gadgets (Id,Name,Type,TypeGroup,Port,Status,Value,ValueUnit,ValueToTargetRatio,ValueToTargetUnit,ComplexValue,Section,SectionPosition,AttachedTo) VALUES (@Id,@Name,@Type,@TypeGroup,@Port,@Status,@Value,@ValueUnit,@ValueToTargetRatio,@ValueToTargetUnit,@ComplexValue,@Section,@SectionPosition,@AttachedTo)";
                }

                public static class Update
                {
                    public const string UpdateValue = "UPDATE Gadgets SET Value = @Value, ComplexValue = @ComplexValue WHERE Id = @Id";
                }

            }

            public static class GadgetAction
            {
                public static class Get
                {
                    public const string IdParam = "SELECT * FROM GadgetActions WHERE Id = @Id";
                    public const string BySourceGadget = "SELECT * FROM GadgetActions WHERE SourceGadget = @SourceGadget";
                }

                public static class Save
                {
                    public const string UpdateWithId = "UPDATE GadgetActions SET 'Order' = @Order, CanExecute = @CanExecute, SourceGadget = @SourceGadget, TargetGadget = @TargetGadget, TargetValue = @TargetValue, TargetComplexValue = @TargetComplexValue WHERE Id = @Id";
                    public const string Insert = "INSERT INTO GadgetActions (Id,'Order',SourceGadget,CanExecute,TargetGadget,TargetValue,TargetComplexValue) VALUES (@Id, @Order,@SourceGadget,@CanExecute,@TargetGadget,@TargetValue,@TargetComplexValue)";
                }


            }
        }
    }
}