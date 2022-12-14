using LittleBitGames.FTUE.Components.Base;

namespace LittleBitGames.FTUE.Kernel.Factory
{
    public interface IComponentFactory
    {
        public T Create<T>(params object[] args) where T : ScenarioComponent;
    }
}