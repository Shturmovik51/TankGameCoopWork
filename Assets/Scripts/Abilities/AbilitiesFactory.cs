using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class AbilitiesFactory
    {
        private List<Ability> _abilities;

        public AbilitiesFactory(GameData gameData)
        {
            _abilities = gameData.AbilityBase.AbilitySamples;
        }

        public Ability GetRandomAbility()
        {
            return _abilities[Random.Range(0, _abilities.Count)];
        }

        public Ability GetAbility(AbilityType type)
        {            
            return _abilities.Find(ability => ability.Type == type);
        }
    }
}