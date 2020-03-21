
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
                    public const string UpdateWithId = "UPDATE Regions SET Name = @Name, Type = @Type WHERE Id = @Id";
                    public const string Insert = "INSERT INTO Regions (Id,Name,Type)VALUES(@Id,@Name,@Type)";
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
                    public const string UpdateWithId = "UPDATE Sections SET Name = @Name, Region = @Region, RightSection = @RightSection, LeftSection = @LeftSection, TopSection = @TopSection, BottomSection = @BottomSection WHERE Id = @Id";
                    public const string Insert = "INSERT INTO Sections (Id,Name,Region,RightSection,LeftSection,TopSection,BottomSection)VALUES(@Id,@Name,@Region,@RightSection,@LeftSection,@TopSection,@BottomSection)";
                }
            }


            public static class Gadget
            {
                public static class Get
                {
                    public const string FilterBySection = "SELECT * FROM Gadgets WHERE Section = @Section";
                    public const string IdParam = "SELECT * FROM Gadgets WHERE Id = @Id";
                }

                public static class Save
                {
                    public const string UpdateWithId = "UPDATE Gadgets SET Name = @Name,Type = @Type,Port = @Port,Status = @Status,Value = @Value,ValueUnit = @ValueUnit,ValueToTargetRatio = @ValueToTargetRatio,ValueToTargetUnit = @ValueToTargetUnit,ComplexValue = @ComplexValue,Section = @Section,SectionPosition = @SectionPosition,AttachedTo = @AttachedTo WHERE Id = @Id";
                    public const string Insert = "INSERT INTO Gadgets (Id,Name,Type,Port,Status,Value,ValueUnit,ValueToTargetRatio,ValueToTargetUnit,ComplexValue,Section,SectionPosition,AttachedTo) VALUES (@Id,@Name,@Type,@Port,@Status,@Value,@ValueUnit,@ValueToTargetRatio,@ValueToTargetUnit,@ComplexValue,@Section,@SectionPosition,@AttachedTo)";
                }

            }


        }
    }
}