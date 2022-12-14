using System;
using LittleBit.Modules.CoreModule;
using LittleBit.Modules.CoreModule.MonoInterfaces;
using LittleBitGames.FTUE.Kernel;
using LittleBitGames.FTUE.Kernel.ScenarioSystem;

namespace LittleBitGames.Kernel.FTUE
{
    public class Bootstrap : IBootstraper
    {
        private readonly ScriptRunner _scriptRunner;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ScenarioLibrary _scenarioLibrary;

        public Bootstrap(ScenarioLibrary scenarioLibrary, ICoroutineRunner coroutineRunner)
        {
            _scenarioLibrary = scenarioLibrary;
            _coroutineRunner = coroutineRunner;

            _scriptRunner = new ScriptRunner(scenarioLibrary.GetScenarios(), coroutineRunner);
        }

        public void Run()
        {
            _scriptRunner.Run();
            OnComplete?.Invoke();
        }

        public event Action OnComplete;
    }
}