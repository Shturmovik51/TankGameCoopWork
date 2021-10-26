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
            var shellStartPosition = playerObject.transform.GetChild(0);
            _playerModel.ShellStartPosition = shellStartPosition;
            playerObject.name = "Player";

            return playerObject.transform;
        }
    }
}