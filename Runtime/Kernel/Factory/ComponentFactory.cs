using LittleBitGames.FTUE.Components;
using LittleBitGames.FTUE.Components.Base;
using LittleBitGames.FTUE.Kernel.ScenarioSystem;

namespace LittleBitGames.FTUE.Kernel.Factory
{
    public class ComponentFactory
    {
        private readonly LQueue<ScenarioComponent> _components;
        private readonly ICreator _creator;
        private readonly CaretakerScenario _caretakerScenario;
        private readonly IComponentFactory _componentFactory;
            
        public ComponentFactory(LQueue<ScenarioComponent> components, ICreator creator, Scenario scenario)
        {
            _components = components;
            _creator = creator;
            _caretakerScenario = creator.Instantiate<CaretakerScenario>(scenario);
        }
        
        public ComponentFactory Create<T>(params object[] args) where T : ScenarioComponent
        {
            var component = _creator.Instantiate<T>(args);
            Enqueue(component);
            return this;
        }

        public ComponentFactory Backup()
        {
            Enqueue(new BackupScenarioComponent(_caretakerScenario));
            return this;
        }
        
        private void Enqueue(ScenarioComponent scenarioComponent)
        {
            _components.Enqueue(scenarioComponent);
        }

 
    }
    
}