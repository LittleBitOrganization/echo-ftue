using LittleBit.Modules.SequenceLogicModule;
using LittleBitGames.FTUE.Components;

namespace LittleBitGames.FTUE.Kernel.Factory
{
    public static class InternalComponentFactoryExtension
    {
        public static ComponentFactory ActionDialog(this ComponentFactory componentFactory, string key)
        {
            return componentFactory.Create<DialogScenarioComponent>(key);
        }

        public static ComponentFactory ActionFocusCamera(this ComponentFactory componentFactory, string key)
        {
            return componentFactory.Create<CameraFocusScenarioComponent>(key);
        }
        
        public static ComponentFactory ActionFocusCameraFollow(this ComponentFactory componentFactory, string key, float time)
        {
            return componentFactory.Create<CameraFocusScenarioComponent>(key, time);
        }

        public static ComponentFactory ActionFocusFinger(this ComponentFactory componentFactory, string key, UnitLogic condition = null)
        {
            return condition != null 
                ? componentFactory.Create<FingerFocusScenarioComponent>(key, condition)
                : componentFactory.Create<FingerFocusScenarioComponent>(key);
        }

        public static ComponentFactory WaitForFocusAppear(this ComponentFactory componentFactory, string key)
        {
            return componentFactory.Create<WaitForFocusAppear>(key);
        }
    }
}