using System.Collections.Generic;

namespace LittleBitGames.FTUE.Kernel.ScenarioSystem
{
    public class ScenarioLibrary
    {
        private LQueue<Scenario> _scenarios;

        public ScenarioLibrary()
        {
            _scenarios = new LQueue<Scenario>();
        }

        public void Append(Scenario scenario) => _scenarios.Enqueue(scenario);

        public void Dequeue() => _scenarios.Dequeue();
        
        public LQueue<Scenario> GetScenarios() => _scenarios;

        public void InsertToFirst(Scenario scenario)
        {
            List<Scenario> scenarios = _scenarios;
            scenarios.Insert(0, scenario);
            _scenarios = (LQueue<Scenario>)scenarios;
        }
    }
}