using System;

namespace LittleBitGames.FTUE.DialogSystem
{
    public interface IDialogView : IDisposable
    {
        event Action OnClick;
        void NextPhrase(string phrase);
    }
}