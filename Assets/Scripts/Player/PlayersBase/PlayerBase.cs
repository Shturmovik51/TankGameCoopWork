using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    [CreateAssetMenu(fileName = "New Player Database", menuName = "DataBase /Players")]

    public sealed class PlayerBase : ScriptableObject
    {
        [SerializeField, HideInInspector] private List<PlayerModelData> _playerSamples;
        [SerializeField] private PlayerModelData _currentPlayer;
        private int _currentNumberInArray;

        public List<PlayerModelData> PlayerSamples => _playerSamples;

        public void CreatePlayer()
        {
            if (_playerSamples == null)
                _playerSamples = new List<PlayerModelData>();
            PlayerModelData playerSample = new PlayerModelData();
            _playerSamples.Add(playerSample);
            _currentPlayer = playerSample;
            _currentNumberInArray = _playerSamples.Count - 1;
        }

        public void RemovePlayer()
        {
            if (_playerSamples == null)
                return;
            if (_playerSamples.Count > 1)
            {
                _playerSamples.Remove(_currentPlayer);

                if (_currentNumberInArray > 0)
                    _currentNumberInArray--;
                else
                    _currentNumberInArray = 0;

                _currentPlayer = _playerSamples[_currentNumberInArray];
            }

            else
            {
                _playerSamples.Remove(_currentPlayer);
                CreatePlayer();
            }
        }

        public void NextPlayer()
        {
            if (_playerSamples.Count > _currentNumberInArray + 1)
            {
                _currentNumberInArray++;
                _currentPlayer = _playerSamples[_currentNumberInArray];
            }
        }

        public void PrevPlayer()
        {
            if (_currentNumberInArray > 0)
            {
                _currentNumberInArray--;
                _currentPlayer = _playerSamples[_currentNumberInArray];
            }
        }

        //public Ability GetItemOfID(int id)
        //{
        //    return _AbilitySamples.Find(ability => ability.ID == id);
        //}
    }
}


