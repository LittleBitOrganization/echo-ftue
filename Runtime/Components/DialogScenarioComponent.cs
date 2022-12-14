using LittleBitGames.FTUE.Components.Base;
using LittleBitGames.FTUE.DialogSystem;

namespace LittleBitGames.FTUE.Components
{
    public class DialogScenarioComponent : ScenarioComponent
    {
        private readonly DialogFactory _dialogFactory;
        private readonly string _key;
        
        private IDialogModel _dialogModel;
        
        public DialogScenarioComponent(DialogFactory dialogFactory, string key)
        {
            _key = key;
            _dialogFactory = dialogFactory;
        }

        protected override void OnExecute()
        {
            _dialogModel = _dialogFactory.Create(_key, true);
            _dialogModel.OnDispose += Complete;
            
            _dialogModel.OnClick();
        }

        protected override void OnComplete()
        {
            
        }

        protected override void OnDispose() =>
            _dialogModel.OnDispose -= Complete;
    }
}