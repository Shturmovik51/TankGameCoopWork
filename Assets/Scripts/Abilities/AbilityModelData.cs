using System;
using UnityEngine;

namespace TankGame
{
    [Serializable]
    public class AbilityModelData : IAbility
    {
        [SerializeField] private AbilityType _type;
        [SerializeField] private int _cD;
        [SerializeField] private Sprite _elementIcon;
        [SerializeField] private Sprite _deathIcon;
        [SerializeField] private int _iD;
        [SerializeField] private string _helpText;

        public AbilityType Type => _type;
        public int CD => _cD;
        public int ID => _iD;
        public Sprite ElementIcon => _elementIcon;
        public Sprite DeathIcon => _deathIcon;
        public string HelpText => _helpText;
        public bool IsActive { get; private set; }
    }
}
