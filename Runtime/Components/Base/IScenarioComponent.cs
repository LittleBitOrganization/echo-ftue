using System;

namespace LittleBitGames.FTUE.Components.Base
{
    internal interface IScenarioComponent
    {
        public event Action OnCompleteEvent;
        public void Execute();

    }
}