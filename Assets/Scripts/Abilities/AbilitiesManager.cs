using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class AbilitiesManager
    {
        private List<Ability> _abilities;
        public List<Ability> Abilities => _abilities;

        public AbilitiesManager(GameData gameData)
        {
            _abilities = gameData.AbilityBase.AbilitySamples;
        }

        public Ability GetAbility(int abilityIndex)
        {            
            return _abilities.Find(ability => ability.ID == abilityIndex);
        }

        public int GetRandomAbilityIndex()
        {
            return _abilities[Random.Range(0, _abilities.Count)].ID;
        }

        public int GetAbilityIndex(AbilityType abilityType)
        {
            return _abilities.Find(ability => ability.Type == abilityType).ID;
        }
    }
}