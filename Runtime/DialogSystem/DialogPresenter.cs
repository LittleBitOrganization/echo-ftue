using System;

namespace LittleBitGames.FTUE.DialogSystem
{
    public class DialogPresenter : IDisposable
    {
        private readonly IDialogModel _dialogModel;
        private readonly IDialogView _dialogView;
        
        public DialogPresenter(IDialogModel dialogModel, IDialogView dialogView)
        {
            _dialogModel = dialogModel;
            _dialogView = dialogView;
            
            Subscribe();
        }

        private void Subscribe()
        {
            _dialogModel.OnNextPhrase += OnNextPhrase;
            _dialogView.OnClick += OnClick;
            _dialogModel.OnDispose += Dispose;
        }

        public void Dispose()
        {
            _dialogModel.OnNextPhrase -= OnNextPhrase;
            _dialogView.OnClick -= OnClick;
            _dialogModel.OnDispose -= Dispose;
            
            _dialogView.Dispose();
        }

        private void OnClick() =>
            _dialogModel.OnClick();

        private void OnNextPhrase(string phrase) =>
            _dialogView.NextPhrase(phrase);
    }
}