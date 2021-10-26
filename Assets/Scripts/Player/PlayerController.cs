using System.Collections;
using UnityEngine;

namespace TankGame
{
    public class PlayerController: IInitializable, ICleanable, IUpdatable, IController
    {
        private PlayerModel _playerModel;
        private PlayerView _playerView;
        private InputController _inputController;
        private Transform _player;
        private PoolController _poolController;
        private GameManager _gameManager;
        private bool _isShootDelay;

        public PlayerController(PlayerModel playerModel, PlayerView playerView, InputController inputController, Transform player,
                       PoolController poolController, GameManager gameManager)
        {
            _playerModel = playerModel;
            _playerView = playerView;
            _inputController = inputController;
            _player = player;
            _poolController = poolController;
            _gameManager = gameManager;
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
            shell.transform.position = _playerModel.ShellStartPosition.position;
            shell.transform.rotation = _player.rotation;
            shell.SetActive(true);
            shell.GetComponent<Rigidbody>().AddForce(_playerModel.ShellStartPosition.forward * 300, ForceMode.Impulse);

            _gameManager.StartCoroutine(ShootDelay());
        }

        private IEnumerator ShootDelay()
        {
            yield return new WaitForSeconds(5);
            _isShootDelay = false;
        }


    }
}