using System;
using LittleBitGames.FTUE.Components.Base;
using LittleBitGames.FTUE.Kernel.Factory;

namespace LittleBitGames.FTUE.Kernel.ScenarioSystem
{
    internal class ScenarioRunner : IRunner
    {
        public event Action OnComplete;
        private readonly LQueue<ScenarioComponent> _queue;
        private readonly ComponentFactory _componentFactory;
        private ScenarioComponent CurrentComponent { get; set; }

        public ScenarioRunner(LQueue<ScenarioComponent> queue, ComponentFactory componentFactory)
        {
            _queue = queue;
            _componentFactory = componentFactory;
        }
        public void Run()
        {
            _componentFactory.Backup();
            Execute();
        }
        
        private void Execute()
        {
            if (_queue.Count > 0)
            {
                CurrentComponent = _queue.Dequeue();
                CurrentComponent.OnCompleteEvent += Next;
                CurrentComponent.Execute();
            }
            else
            {
                OnComplete?.Invoke();
            }
        }

        private void Next()
        {
            CurrentComponent.OnCompleteEvent -= Next;
            Execute();
        }
        
    }
}