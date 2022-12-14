using LittleBitGames.FTUE.Components.Base;
using LittleBitGames.FTUE.HighlightSystem;

namespace LittleBitGames.FTUE.Components
{
    public class WaitForFocusAppear : ScenarioComponent
    {
        private readonly string _key;
        private readonly ContainerHighlighterObjects _container;

        public WaitForFocusAppear(string key, ContainerHighlighterObjects container)
        {
            _key = key;
            _container = container;
        }
        protected override void OnExecute()
        {
            if (_container.Contains(_key) == false)
            {
                _container.Subscribe(_key, OnAdded);
            }
            else
            {
                Complete();
            }
        }

        private void OnAdded(IHighlighter obj)
        {
            _container.Unsubscribe(_key, OnAdded);
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