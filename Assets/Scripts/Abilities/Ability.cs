using System;
using UnityEngine;

namespace TankGame
{
    [Serializable]
    public class Ability : IAbility
    {
        [SerializeField] private AbilityType _type;
        [SerializeField] private int _iD;
        [SerializeField] private Sprite _icon;

        public AbilityType Type => _type;
        public int ID => _iD;
        public Sprite Icon => _icon;
        public bool IsActive { get; private set; }       
    }
}