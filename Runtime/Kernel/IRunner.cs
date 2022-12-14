using System;

namespace LittleBitGames.FTUE.Kernel
{
    internal interface IRunner
    {
        public void Run();
        public event Action OnComplete;
    }
}