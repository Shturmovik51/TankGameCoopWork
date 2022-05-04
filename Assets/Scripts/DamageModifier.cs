using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class DamageModifier
    {
        private const int TWICE_DAMAGE_MULTIPLIER = 2;
        private const int NORMAL_DAMAGE_MULTIPLIER = 1;

        public int GetModifier(AbilityType owner, AbilityType target)
        {
            if (owner == AbilityType.FireAbility && target == AbilityType.GroundAbility ||
                owner == AbilityType.WaterAbility && target == AbilityType.FireAbility ||
                owner == AbilityType.GroundAbility && target == AbilityType.WaterAbility)
                return TWICE_DAMAGE_MULTIPLIER;
            else
                return NORMAL_DAMAGE_MULTIPLIER;
        }
    }
}