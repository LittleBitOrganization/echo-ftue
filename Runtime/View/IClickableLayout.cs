using System;

namespace LittleBitGames.FTUE.View
{
    public interface IClickableLayout
    {
        public void AddOnClickListener(Action onClick);
        public void RemoveOnClickListener(Action onClick);
    }
}