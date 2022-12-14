using System;
using LittleBitGames.FTUE.Components.Base;

namespace LittleBitGames.FTUE.Components
{
    public class DoScenarioComponent : ScenarioComponent
    {
        private readonly Action _doAction;

        public DoScenarioComponent(Action doAction)
        {
            _doAction = doAction;
        }
        protected override void OnExecute()
        {
            _doAction.Invoke();
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