using LittleBit.Modules.CoreModule;
using LittleBitGames.FTUE.Kernel.ScenarioSystem;

namespace LittleBitGames.FTUE.Components
{
    public class CaretakerScenario
    {
        private readonly Scenario _scenario;
        private readonly IDataStorageService _dataStorageService;

        public CaretakerScenario(Scenario scenario, IDataStorageService dataStorageService)
        {
            _scenario = scenario;
            _dataStorageService = dataStorageService;
            Restore();
        }

        public void Backup()
        {
            _dataStorageService.SetData(_scenario.GetKey(), _scenario.Backup());
        }
        
        private void Restore()
        {
            var data = _dataStorageService.GetData<ScenarioData>(_scenario.GetKey());
            _scenario.Restore(data);
        }
        
        
        
    }
}