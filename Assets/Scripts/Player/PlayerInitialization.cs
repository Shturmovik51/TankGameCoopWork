using UnityEngine;

namespace TankGame
{
    public class PlayerInitialization
    {
        private PlayerFactory _playerFactory;
        private Transform _player;

        public PlayerInitialization(PlayerFactory playerFactory, Transform playerPosition)
        {
            _playerFactory = playerFactory;
            _player = _playerFactory.CreatePlayer();
            _player.position = playerPosition.position;
        }

        public Transform GetPlayer()
        {            
            return _player;
        }
    }
}