using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;


namespace sp.iot.core
{
    public class ScriptingService : IScriptingService
    {
        private readonly IConfiguration _config;

        private Dictionary<Guid, ScriptRunner> runners;

        public ScriptingService(IConfiguration config)
        {
            _config = config;
            runners = new Dictionary<Guid, ScriptRunner>();
        }

        public ScriptResult Execute(GadgetAction action, ScriptParameter param)
        {
            var returnValue = new ScriptResult();
            var runner = _getRunner(action.Id);

            returnValue.CanExecute = _canExecute(runner, action, param);

            if (returnValue.CanExecute)
            {
                returnValue.TargetNewValue = _targetNewValue(runner, action, param);
            }
            return returnValue;
        }

        private bool _canExecute(ScriptRunner runner, GadgetAction action, ScriptParameter param)
        {
            if (runner.CanExecuteScript != action.CanExecute)
            {
                var script = CSharpScript.Create<bool>(action.CanExecute, globalsType: typeof(ScriptParameter));
                runner.CanExecuteRunner = script.CreateDelegate();
                runner.CanExecuteScript = action.CanExecute;
            }

            var execution = runner.CanExecuteRunner(param);
            return execution.Result;
        }

        private double _targetNewValue(ScriptRunner runner, GadgetAction action, ScriptParameter param)
        {

            if (runner.TargetNewValueScript != action.Execute)
            {
                var script = CSharpScript.Create<double>(action.Execute, globalsType: typeof(ScriptParameter));
                runner.TargetNewValueRunner = script.CreateDelegate();
                runner.TargetNewValueScript = action.Execute;
            }

            var execution = runner.TargetNewValueRunner(param);
            return execution.Result;
        }

        private ScriptRunner _getRunner(Guid id)
        {
            ScriptRunner runner = null;
            if (runners.ContainsKey(id))
            {
                runner = runners[id];
            }
            else
            {
                runner = new ScriptRunner();
                runners.Add(id, runner);
            }
            return runner;
        }
    }
}