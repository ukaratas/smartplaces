using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace sp.iot.core
{
    public class GadgetService : IGadgetService

    {
        private readonly IConfiguration _config;
        private readonly IDatabase _database;

        private readonly IServiceProvider _services;

        private readonly IGadgetActionService _gadgetActionService;

        private readonly IScriptingService _scriptingService;

        public GadgetService(IConfiguration config, IDatabase database, IGadgetActionService gadgetActionService, IServiceProvider services, IScriptingService scriptingService)
        {
            _config = config;
            _database = database;
            _gadgetActionService = gadgetActionService;
            _services = services;
            _scriptingService = scriptingService;

        }

        public Gadget Get(Guid id, bool includeActions)
        {
            Gadget gadget = null;
            SqliteDataReader reader = _database.ExecuteReader(
                ConstantStrings.SqlQueries.Gadget.Get.IdParam,
                new List<SqliteParameter> { new SqliteParameter("Id", id) }
                );

            if (reader.Read())
            {
                gadget = BindGadgetData(reader);

                if (includeActions)
                {
                    gadget.Actions.AddRange(_gadgetActionService.GetByGadget(id));
                }
            }
            return gadget;
        }

        public List<Gadget> GetBySection(Guid section, bool includeActions)
        {
            var returnValue = new List<Gadget>();

            SqliteDataReader reader = _database.ExecuteReader(
                ConstantStrings.SqlQueries.Gadget.Get.FilterBySection,
                new List<SqliteParameter> { new SqliteParameter("Section", section) }
                );

            while (reader.Read())
            {
                var gadget = BindGadgetData(reader);

                if (includeActions)
                {
                    gadget.Actions.AddRange(_gadgetActionService.GetByGadget(reader.GetValueAsGuid("Id")));
                }

                returnValue.Add(gadget);
            }
            return returnValue;
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

            returnValue.AddAction(string.Format("Set Value '{0}' started with values {1}, {2}", id, value.Value, value.ComplexValue));

            ProcessGadget(
                id,
                value,
                (log) => { returnValue.AddAction(string.Format("Process for '{0}' :{1}", id, log)); }
                );

            return returnValue;
        }

        public void ProcessGadget(Guid id, GadgetSetValueRequest value, Action<string> logCallback)
        {
            Gadget gadget = Get(id, false);

            if (gadget == null)
            {
                logCallback(string.Format("Gadget '{0}' not found.", id));
                return;
            }
            else
            {
                logCallback(string.Format("Gadget {0} is found. Item will be updated.", gadget.Name));
            }

            _database.ExecuteScalar<string>(
               ConstantStrings.SqlQueries.Gadget.Update.UpdateValue,
               new List<SqliteParameter> {
                    new SqliteParameter("Id", id),
                    new SqliteParameter("Value", value.Value),
                    new SqliteParameter("ComplexValue", value.ComplexValue)
                   }
               );

            logCallback(string.Format("Gadget {0} is updated with values ({1},{2}).", gadget.Name, value.Value, value.ComplexValue));

            var actions = _gadgetActionService.GetByGadget(id);

            actions.ForEach(action =>
            {
                logCallback(string.Format("Action {0} - ({1}) checking.", action.Order, action.Id));

                Gadget targetGadget = Get(action.TargetGadget, false);

                var scriptResult = _scriptingService.Execute(
                    action,
                    new ScriptParameter()
                    {
                        SourceNewValue = value.Value,
                        SourceNewComplexValue = value.ComplexValue,
                        SourceOldValue = gadget.Value,
                        SourceOldComplexValue = gadget.ComplexValue,
                        TargetOldValue = targetGadget.Value,
                        TargetOldComplexValue = targetGadget.ComplexValue
                    });

                if (scriptResult.CanExecute)
                {
                    ProcessGadget(action.TargetGadget, new GadgetSetValueRequest() { Value = scriptResult.TargetNewValue, ComplexValue = scriptResult.TargetNewComplexValue }, logCallback);
                }
            });
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