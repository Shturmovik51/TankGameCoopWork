using System;
using UnityEngine;

namespace TankGame
{
    public class Ability : IAbility
    {
        public AbilityType Type { get; }
        public int CD { get; }
        public Sprite ElementIcon { get; }
        public Sprite DeathIcon { get; }
        public int ID { get; }
        public bool IsActive { get; private set; }  
        
        public Ability (AbilityType type, int cD, Sprite elementIcon, Sprite deathIcon, int iD)
        {
            Type = type;
            CD = cD;
            ElementIcon = elementIcon;
            DeathIcon = deathIcon;
            ID = iD;
        }
    }
}