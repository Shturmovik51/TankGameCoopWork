using System;
using UnityEngine;

namespace TankGame
{
    [Serializable]
    public class Ability : IAbility
    {
        [SerializeField] private AbilityType _type;
        [SerializeField] private int _cD;
        [SerializeField] private Sprite _icon;
        private int _iD;

        public AbilityType Type => _type;
        public int CD => _cD;
        public int ID => _iD;
        public Sprite Icon => _icon;
        public bool IsActive { get; private set; }       
    }
}