using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    [CreateAssetMenu(fileName = "New Enemy Database", menuName = "DataBase /Enemies")]

    public sealed class EnemyBase : ScriptableObject
    {
        [SerializeField, HideInInspector] private List<EnemyModelData> _enemySamples;
        [SerializeField] private EnemyModelData _currentEnemy;
        private int _currentNumberInArray;

        public List<EnemyModelData> EnemySamples => _enemySamples;

        public void CreateEnemy()
        {
            if (_enemySamples == null)
                _enemySamples = new List<EnemyModelData>();
            EnemyModelData enemySample = new EnemyModelData();
            _enemySamples.Add(enemySample);
            _currentEnemy = enemySample;
            _currentNumberInArray = _enemySamples.Count - 1;
        }

        public void RemoveEnemy()
        {
            if (_enemySamples == null)
                return;
            if (_enemySamples.Count > 1)
            {
                _enemySamples.Remove(_currentEnemy);

                if (_currentNumberInArray > 0)
                    _currentNumberInArray--;
                else
                    _currentNumberInArray = 0;

                _currentEnemy = _enemySamples[_currentNumberInArray];
            }

            else
            {
                _enemySamples.Remove(_currentEnemy);
                CreateEnemy();
            }
        }

        public void NextEnemy()
        {
            if (_enemySamples.Count > _currentNumberInArray + 1)
            {
                _currentNumberInArray++;
                _currentEnemy = _enemySamples[_currentNumberInArray];
            }
        }


        public void PrevEnemy()
        {
            if (_currentNumberInArray > 0)
            {
                _currentNumberInArray--;
                _currentEnemy = _enemySamples[_currentNumberInArray];
            }
        }

        //public Ability GetItemOfID(int id)
        //{
        //    return _AbilitySamples.Find(ability => ability.ID == id);
        //}
    }
}

