using System.Collections;
using UnityEngine;

namespace TankGame
{
    public class PlayerController: IInitializable, ICleanable, IUpdatable, IController
    {
        private PlayerModel _playerModel;
        private PlayerView _playerView;
        private InputController _inputController;
        private PoolController _poolController;
        private bool _isShootDelay;

        public PlayerController(PlayerModel playerModel, PlayerView playerView, InputController inputController,
                    PoolController poolController)
        {
            _playerModel = playerModel;
            _playerView = playerView;
            _inputController = inputController;
            _poolController = poolController;
        }
        public void Initialization()
        {
            _inputController.OnClickShootButton += PlayerShoot;
        }

        public void CleanUp()
        {
            _inputController.OnClickShootButton -= PlayerShoot;
        }

        public void LocalUpdate(float deltaTime)
        {

        }

        private void PlayerShoot()
        {
            if (_isShootDelay) return;

            _isShootDelay = true;
            var shell = _poolController.GetShell();
            _playerView.Shoot(shell, _playerModel.ShootForce);
            _playerView.StartCoroutine(ShootDelay());
        }

        private IEnumerator ShootDelay()
        {
            yield return new WaitForSeconds(5);
            _isShootDelay = false;
        }


    }
}