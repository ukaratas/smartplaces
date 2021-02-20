using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;


namespace sp.iot.core
{
    public class GadgetActionService : IGadgetActionService
    {
        private readonly IConfiguration _config;
        private readonly IDatabase _database;

        public GadgetActionService(IConfiguration config, IDatabase database)
        {
            _config = config;
            _database = database;
        }


        public List<GadgetAction> GetByGadgetSource(Guid gadgetId)
        {
            List<GadgetAction> returnValue = new List<GadgetAction>();

            SqliteDataReader reader = _database.ExecuteReader(
               ConstantStrings.SqlQueries.GadgetAction.Get.BySourceGadget,
               new List<SqliteParameter> { new SqliteParameter("SourceGadget", gadgetId) }
               );

            while (reader.Read())
            {
                var action = BindGadgetActionData(reader);
                returnValue.Add(action);
            }

            return returnValue;
        }

          public List<GadgetAction> GetByGadgetTarget(Guid gadgetId)
        {
            List<GadgetAction> returnValue = new List<GadgetAction>();

            SqliteDataReader reader = _database.ExecuteReader(
               ConstantStrings.SqlQueries.GadgetAction.Get.ByTargetGadget,
               new List<SqliteParameter> { new SqliteParameter("TargetGadget", gadgetId) }
               );

            while (reader.Read())
            {
                var action = BindGadgetActionData(reader);
                returnValue.Add(action);
            }

            return returnValue;
        }

        public GadgetAction BindGadgetActionData(SqliteDataReader reader)
        {
            GadgetAction returnValue = new GadgetAction
            {
                Id = reader.GetValueAsGuid("Id"),
                Order = (int)(long)reader.GetValue(reader.GetOrdinal("Order")),
                TargetGadget = reader.GetValueAsGuid("TargetGadget"),
                SourceGadget = reader.GetValueAsGuid("SourceGadget"),
                CanExecute = reader.GetValue(reader.GetOrdinal("CanExecute")).ToString(),
                Execute = reader.GetValue(reader.GetOrdinal("Execute")).ToString(),
            };
            return returnValue;
        }
    }
}