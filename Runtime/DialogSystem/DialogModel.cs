using System;
using System.Collections.Generic;

namespace LittleBitGames.FTUE.DialogSystem
{
    public class DialogModel : IDisposable, IDialogModel
    {
        public event Action OnDispose;
        public event Action<string> OnNextPhrase;

        private readonly bool _autoHide;
        private readonly Queue<string> _phrases;

        private int _clicks;

        public DialogModel(Queue<string> phrases, bool autoHide)
        {
            _phrases = phrases;
            _autoHide = autoHide;
        }

        public void OnClick()
        {
            if (_phrases.Count is 0 && _autoHide) Dispose();

            OnNextPhrase?.Invoke(_phrases.Dequeue());
        }

        public void Dispose() => OnDispose?.Invoke();
    }
}