using System;
using System.Collections.Generic;
using LittleBit.Modules.Description;
using UnityEngine;

namespace LittleBitGames.FTUE.Configs
{
    [CreateAssetMenu(fileName = "Dialogs", menuName = "Configs/Dialogs/DialogsConfig", order = 0)]
    public class DialogsConfigSO : ScriptableObject
    {
        [SerializeField] private List<DialogConfig> _dialogConfigs;

        [field: SerializeField] public GameObject ViewPrefab { get; private set; }
        
        public IReadOnlyList<DialogConfig> DialogConfigs => _dialogConfigs;
    }

    [Serializable]
    public class DialogConfig : IKeyHolder
    {
        [SerializeField] private string _key;
        [SerializeField] private List<string> _phrases;
        public string GetKey() => _key;
        public IReadOnlyList<string> GetPhrases() => _phrases;
    }

    [CreateAssetMenu(fileName = "DialogSystemSettings", menuName = "Configs/Dialogs/Dialog System Settings")]
    public class DialogSystemSettings : ScriptableObject
    {
        [field: SerializeField] public Sprite MascotSprite { get; private set; }
    }
    
    
}