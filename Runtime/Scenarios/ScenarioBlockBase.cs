using LittleBitGames.FTUE.Kernel.ScenarioSystem;

namespace LittleBitGames.FTUE.Scenarios
{
    public abstract class ScenarioBlockBase
    {
        protected readonly ScenarioLibrary _scenarioLibrary;
        private readonly ScenarioFactory _scenarioFactory;

        public ScenarioBlockBase(ScenarioLibrary scenarioLibrary, ScenarioFactory scenarioFactory)
        {
            _scenarioLibrary = scenarioLibrary;
            _scenarioFactory = scenarioFactory;
        }

        public void Initialize() => CreateScenarios();

        protected abstract void CreateScenarios();
        protected void AppendScenario(Scenario scenario) => _scenarioLibrary.Append(scenario);
        protected Scenario NewScenario(string key) => _scenarioFactory.Create("scenario/" + key);
    }
}