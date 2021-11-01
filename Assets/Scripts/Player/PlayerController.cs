using System;
using System.Collections;
using UnityEngine;

namespace TankGame
{
    public class PlayerController: IInitializable, ICleanable, IUpdatable, IController
    {
        public TargetProvider TargetProvider;
        public EnemyView[] _enemies;

        private PlayerModel _playerModel;
        private PlayerView _playerView;
        private InputController _inputController;
        private PoolController _poolController;
        private bool _isShootDelay;
        private int _takeDamageCount;

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
            _playerView.OnTakeDamage += TakeDamage;            
        }

        public void CleanUp()
        {
            _inputController.OnClickShootButton -= PlayerShoot;
            _playerView.OnTakeDamage -= TakeDamage;
        }

        public void LocalUpdate(float deltaTime)
        {
            RotateToTarget(deltaTime);
        }       

        private void RotateToTarget(float deltatime)
        {
            _playerView.Rotate(TargetProvider.GetTarget(), deltatime);            
        }

        private void PlayerShoot()
        {
            if (_isShootDelay) return;

            _isShootDelay = true;
            var shell = _poolController.GetShell();
            shell.GetComponent<Shell>().SetDamageValue(_playerModel.ShootDamageForce);
            _playerView.Shoot(shell, _playerModel.ShootLaunchForce);            
        }

        private void TakeDamage(int value, IDamagable iD)
        {
            _takeDamageCount++;
            if (_takeDamageCount == _enemies.Length)
            {
                _isShootDelay = false;
                _takeDamageCount = 0;
            }

            _playerModel.Health -= value;
            Debug.Log($"PlayerHealth {_playerModel.Health}");

            _playerView.OnChangeTurn?.Invoke();
        }

    }
}