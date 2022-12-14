using System;

namespace LittleBitGames.FTUE.DialogSystem
{
    public interface IDialogModel
    {
        event Action OnDispose;
        event Action<string> OnNextPhrase;
        void OnClick();
    }
}