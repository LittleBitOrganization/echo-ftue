using LittleBit.Modules.Description;
using UnityEngine;

namespace LittleBitGames.FTUE.HighlightSystem
{
    public interface IFocusable : IKeyHolder 
    {
        public GameObject Object { get; }
    }
}