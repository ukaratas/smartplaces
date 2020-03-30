using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;


namespace sp.iot.core
{
    public class LevelAnalog190Ohm : IGadgetEngine
    {
        private readonly IConfiguration _config;
        private readonly IDatabase _database;
        private readonly IGadgetService _gadgetService;
        private readonly IGadgetActionService _gadgetActionService;

        public LevelAnalog190Ohm(IConfiguration config, IDatabase database, IGadgetService gadgetService, IGadgetActionService gadgetActionService)
        {
            _config = config;
            _database = database;
            _gadgetActionService = gadgetActionService;
            _gadgetService = gadgetService;
        }

        public void Execute(Guid id, GadgetSetValueRequest value, Action<string> logCallback)
        {
            /*
            var actions = _gadgetActionService.GetByGadget(id);

            actions.ForEach(action =>
            {
                if (action.SourceValue == value.Value)
                {
                    _gadgetService.ProcessGadget(action.TargetGadget, new GadgetSetValueRequest() { Value = action.TargetValue }, log);
                }
            });
            */
        }

    }
}