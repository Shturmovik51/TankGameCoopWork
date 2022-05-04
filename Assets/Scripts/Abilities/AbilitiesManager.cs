using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class AbilitiesManager
    {
        private Dictionary<GameObject, Ability> _abilities;
        public Dictionary<GameObject, Ability> Abilities => _abilities;

        public AbilitiesManager(/*GameData gameData*/)
        {
            _abilities = new Dictionary<GameObject, Ability>();//gameData.AbilityBase.AbilitySamples;
        }

        public void AddAbility(GameObject key, Ability ability)
        {
            _abilities.Add(key, ability);
        }

        public Ability GetAbility(GameObject key)
        {            
            _abilities.TryGetValue(key, out Ability value);
            return value;
        }

        //public int GetRandomAbilityIndex()
        //{
        //    //return _abilities[Random.Range(0, _abilities.Count)].ID;
        //}

        //public int GetAbilityIndex(AbilityType abilityType)
        //{
        //    //return _abilities.Find(ability => ability.Type == abilityType).ID;
        //}
    }
}