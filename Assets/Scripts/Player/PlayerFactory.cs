using UnityEngine;

namespace TankGame
{
    public class PlayerFactory
    {
        private IPlayerModel _playerModel;

        public PlayerFactory(IPlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        public Transform CreatePlayer()
        {
            var playerObject = Object.Instantiate(_playerModel.Tank);
            playerObject.name = "Player1";
            playerObject.AddComponent<CharacterController>();
            playerObject.AddComponent<BoxCollider>();

            return playerObject.transform;
        }
    }
}