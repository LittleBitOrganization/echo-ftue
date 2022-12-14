using System;
using LittleBitGames.FTUE.Kernel.Factory;

namespace LittleBitGames.FTUE.Components
{
    public static class ComponentFactoryExtension
    {
        public static ComponentFactory Do(this ComponentFactory componentFactory, Action doAction)
        {
            return componentFactory.Create<DoScenarioComponent>(doAction);
        }

        public static ComponentFactory ActionAnalytic(this ComponentFactory componentFactory, string eventKey)
        {
            return componentFactory.Create<AnalyticScenarioComponent>(eventKey);
        }
    }
}