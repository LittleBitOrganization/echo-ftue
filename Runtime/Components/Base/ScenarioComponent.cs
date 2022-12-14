using System;

namespace LittleBitGames.FTUE.Components.Base
{
    public abstract class ScenarioComponent : IScenarioComponent
    {
        public event Action OnCompleteEvent;
        protected abstract void OnExecute();
        protected abstract void OnComplete();
        protected abstract void OnDispose();


        public void Execute()
        {
            OnExecute();
        }

        protected void Complete()
        {
            OnComplete();
            OnCompleteEvent?.Invoke();
            Dispose();
        }

        private void Dispose()
        {
            OnDispose();
        }
    }
}