using LittleBit.Modules.SequenceLogicModule;
using LittleBitGames.FTUE.Components.Base;
using LittleBitGames.FTUE.HighlightSystem;

namespace LittleBitGames.FTUE.Components
{
    public class FingerFocusScenarioComponent : ScenarioComponent
    {
        private readonly string _key;
        private readonly ContainerHighlighterObjects _containerHighlighterObjects;
        private readonly HighlightPresenter _highlightPresenter;
        private readonly CameraPresenter _cameraPresenter;
        private readonly UnitLogic _condition;

        public FingerFocusScenarioComponent(ContainerHighlighterObjects containerHighlighterObjects, 
                                            HighlightPresenter highlightPresenter, 
                                            CameraPresenter cameraPresenter, 
                                            string key, 
                                            UnitLogic condition = null)
        {
            _key = key;
            _containerHighlighterObjects = containerHighlighterObjects;
            _highlightPresenter = highlightPresenter;
            _cameraPresenter = cameraPresenter;
            _condition = condition;
        }

        protected override void OnExecute()
        {
            if (_containerHighlighterObjects.Contains(_key) == false)
            {
                _containerHighlighterObjects.Subscribe(_key, FocusTo);
            }
            else
            {
                var highlighter = _containerHighlighterObjects.Get(_key);
                FocusTo(highlighter);
            }
        }

        private void FocusTo(IHighlighter highlighter)
        {
            if (_condition != null)
                highlighter.AddClickListener(() => TryComplete(highlighter));
            else
                highlighter.AddClickListener(Complete);

            _containerHighlighterObjects.Unsubscribe(_key, FocusTo);
            FocusOnObject(highlighter);
        }

        private void TryComplete(IHighlighter highlighter)
        {
            if (_condition.IsAvailable)
            {
                _highlightPresenter.Unfocus();
                _cameraPresenter.EnableCamera();
                Complete();
            }
            else
            {
                FocusOnObject(highlighter);
            }
        }

        private void FocusOnObject(IHighlighter highlighter)
        {
            _highlightPresenter.FocusTo(highlighter);
            _cameraPresenter.FocusTo(highlighter, null);
            _cameraPresenter.DisableCamera();
        }

        protected override void OnComplete()
        {
            _highlightPresenter.Unfocus();
            _cameraPresenter.EnableCamera();
        }

        protected override void OnDispose()
        {
            //_containerHighlighterObjects.Unsubscribe(_key, FocusTo);
            //throw new NotImplementedException();
        }
    }
    
}