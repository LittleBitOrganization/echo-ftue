using LittleBitGames.FTUE.Components.Base;

namespace LittleBitGames.FTUE.Components
{
    public class BackupScenarioComponent : ScenarioComponent
    {
        private readonly CaretakerScenario _caretakerScenario;
        
        public BackupScenarioComponent(CaretakerScenario caretakerScenario)
        {
            _caretakerScenario = caretakerScenario;

        }
        protected override void OnExecute()
        {
            _caretakerScenario.Backup();
            Complete();
        }

        protected override void OnComplete()
        {
            
        }

        protected override void OnDispose()
        {
            
        }
    }
    
}