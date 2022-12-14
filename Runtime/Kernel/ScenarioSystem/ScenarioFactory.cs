using LittleBitGames.QuestsModule.Factories;

namespace LittleBitGames.FTUE.Kernel.ScenarioSystem
{
    public class ScenarioFactory
    {
        private readonly ICreator _creator;
        private readonly ITrackerProvider _trackerProvider;

        public ScenarioFactory(ICreator creator, ITrackerProvider trackerProvider)
        {
            _creator = creator;
            _trackerProvider = trackerProvider;
        }

        public Scenario Create(string key) => 
            new(_creator, _trackerProvider, key);
    }
}