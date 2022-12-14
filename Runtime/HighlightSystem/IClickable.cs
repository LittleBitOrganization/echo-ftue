using System;

namespace LittleBitGames.FTUE.HighlightSystem
{
    public interface IClickable
    {
        public void AddClickListener(Action onClick);
        public void RemoveClickListener(Action onClick);
    }
}