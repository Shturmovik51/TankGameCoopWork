using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    [CreateAssetMenu(fileName = "New Ability Database", menuName = "DataBase /Abilities")]

    public sealed class AbilityBase : ScriptableObject
    {
        [SerializeField, HideInInspector] private List<AbilityModelData> _abilitySamples;
        [SerializeField] private AbilityModelData _currentAbility;
        private int _currentNumberInArray;

        public List<AbilityModelData> AbilitySamples => _abilitySamples;

        public void CreateAbility()
        {
            if (_abilitySamples == null)
                _abilitySamples = new List<AbilityModelData>();
            AbilityModelData abilitySample = new AbilityModelData();
            _abilitySamples.Add(abilitySample);
            _currentAbility = abilitySample;
            _currentNumberInArray = _abilitySamples.Count - 1;
        }

        public void RemoveAbility()
        {
            if (_abilitySamples == null)
                return;
            if (_abilitySamples.Count > 1)
            {
                _abilitySamples.Remove(_currentAbility);

                if (_currentNumberInArray > 0)
                    _currentNumberInArray--;
                else
                    _currentNumberInArray = 0;

                _currentAbility = _abilitySamples[_currentNumberInArray];
            }

            else
            {
                _abilitySamples.Remove(_currentAbility);
                CreateAbility();
            }
        }

        public void NextAbility()
        {
            if (_abilitySamples.Count > _currentNumberInArray + 1)
            {
                _currentNumberInArray++;
                _currentAbility = _abilitySamples[_currentNumberInArray];
            }
        }


        public void PrevAbility()
        {
            if (_currentNumberInArray > 0)
            {
                _currentNumberInArray--;
                _currentAbility = _abilitySamples[_currentNumberInArray];
            }
        }

        //public Ability GetItemOfID(int id)
        //{
        //    return _AbilitySamples.Find(ability => ability.ID == id);
        //}
    }
}
