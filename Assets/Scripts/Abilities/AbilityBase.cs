using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    [CreateAssetMenu(fileName = "New Ability Database", menuName = "DataBase /Abilities")]

    public sealed class AbilityBase : ScriptableObject
    {
        [SerializeField, HideInInspector] private List<Ability> _AbilitySamples;
        [SerializeField] private Ability _currentAbility;
        private int _currentNumberInArray;

        public List<Ability> AbilitySamples => _AbilitySamples;

        public void CreateAbility()
        {
            if (_AbilitySamples == null)
                _AbilitySamples = new List<Ability>();
            Ability AbilitySample = new Ability();
            _AbilitySamples.Add(AbilitySample);
            _currentAbility = AbilitySample;
            _currentNumberInArray = _AbilitySamples.Count - 1;
        }

        public void RemoveAbility()
        {
            if (_AbilitySamples == null)
                return;
            if (_AbilitySamples.Count > 1)
            {
                _AbilitySamples.Remove(_currentAbility);

                if (_currentNumberInArray > 0)
                    _currentNumberInArray--;
                else
                    _currentNumberInArray = 0;

                _currentAbility = _AbilitySamples[_currentNumberInArray];
            }

            else
            {
                _AbilitySamples.Remove(_currentAbility);
                CreateAbility();
            }
        }

        public void NextAbility()
        {
            if (_AbilitySamples.Count > _currentNumberInArray + 1)
            {
                _currentNumberInArray++;
                _currentAbility = _AbilitySamples[_currentNumberInArray];
            }
        }


        public void PrevAbility()
        {
            if (_currentNumberInArray > 0)
            {
                _currentNumberInArray--;
                _currentAbility = _AbilitySamples[_currentNumberInArray];
            }
        }

        //public Ability GetItemOfID(int id)
        //{
        //    return _AbilitySamples.Find(ability => ability.ID == id);
        //}
    }
}
