using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;


namespace sp.iot.core
{
    public class GadgetService : IGadgetService
    {
        private readonly IConfiguration _config;
        private readonly IDatabase _database;

        public GadgetService(IConfiguration config, IDatabase database)
        {
            _config = config;
            _database = database;
        }

        public Gadget Get(Guid id)
        {
            Gadget gadget = null;
            SqliteDataReader reader = _database.ExecuteReader(
                ConstantStrings.SqlQueries.Gadget.Get.IdParam,
                new List<SqliteParameter> { new SqliteParameter("Id", id) }
                );

            if (reader.Read())
            {
                gadget = BindGadgetData(reader);
            }
            return gadget;
        }

        public IEnumerable<Gadget> GetFiltered(Guid region, Guid section, GadgetTypeGroup? typeGroup, GadgetType? type)
        {
            List<Gadget> returnValue = new List<Gadget>();

            var filter = string.Empty;
            var parameters = new List<SqliteParameter>();


            if (region != Guid.Empty)
            {
                filter = " Region = @Region";
                parameters.Add(new SqliteParameter("Region", region));
            }


            if (section != Guid.Empty)
            {
                filter = " Section = @section";
                parameters.Add(new SqliteParameter("Section", section));
            }

            if (typeGroup != null)
            {
                filter = " TypeGroup = @TypeGroup";
                parameters.Add(new SqliteParameter("TypeGroup", typeGroup));
            }

            if (type != null)
            {
                filter = " Type = @Type";
                parameters.Add(new SqliteParameter("Type", type));
            }

            if (filter != string.Empty) filter = " WHERE" + filter;

            SqliteDataReader reader = _database.ExecuteReader(ConstantStrings.SqlQueries.Gadget.Get.AllIncludeRegion + filter, parameters);

            while (reader.Read())
            {
                returnValue.Add(BindGadgetData(reader));
            }
            return returnValue;
        }

        public SaveResponse SetValue(Guid id, GadgetSetValueRequest value)
        {

            SaveResponse returnValue = new SaveResponse();

            _database.ExecuteScalar<string>(
                ConstantStrings.SqlQueries.Gadget.Update.UpdateValue,
                new List<SqliteParameter> {
                    new SqliteParameter("Id", id),
                    new SqliteParameter("Value", value.Value),
                    new SqliteParameter("ComplexValue", value.ComplexValue)
                    }
                );

            returnValue.Status = SaveResponseType.Updated;

            return returnValue;
        }

        public Gadget BindGadgetData(SqliteDataReader reader)
        {
            Gadget returnValue = new Gadget
            {
                Id = reader.GetValueAsGuid("Id"),
                Name = reader.GetValue(reader.GetOrdinal("Name")).ToString(),
                TypeGroup = (GadgetTypeGroup)(int)(long)reader.GetValue(reader.GetOrdinal("TypeGroup")),
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