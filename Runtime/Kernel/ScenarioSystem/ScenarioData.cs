using System;
using LittleBit.Modules.CoreModule;
using UnityEngine;

namespace LittleBitGames.FTUE.Kernel.ScenarioSystem
{
    [Serializable]
    public class ScenarioData : Data
    {
        [field: SerializeField] public bool CanBeSkipped { get; set; }

        public ScenarioData()
        {
            CanBeSkipped = false;
        }
    }
}