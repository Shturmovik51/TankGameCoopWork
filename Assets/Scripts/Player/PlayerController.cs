using System;
using System.Collections;
using UnityEngine;

namespace TankGame
{
    public class PlayerController: IInitializable, ICleanable, IUpdatable, IController
    {
        public event Action OnShoot;
        public TargetProvider TargetProvider;
        public EnemyView[] _enemies;

        private PlayerModel _playerModel;
        private PlayerView _playerView;
        private InputController _inputController;
        private PoolController _poolController;
        private bool _isShootDelay;
        private int _takeDamageCount;
        private DamageModifier _damageModifier;

        public PlayerController(PlayerModel playerModel, PlayerView playerView, InputController inputController,
                    PoolController poolController, DamageModifier damageModifier)
        {
            _playerModel = playerModel;
            _playerView = playerView;
            _inputController = inputController;
            _poolController = poolController;
            _damageModifier = damageModifier;
        }
        public void Initialization()
        {
            UpdateHealthBar();

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
            if (!_playerModel.IsAbilityActive) return;

            OnShoot?.Invoke();
            _isShootDelay = true;
            var shell = _poolController.GetShell();
            shell.GetComponent<Shell>().SetDamageValue(_playerModel.ShootDamageForce, _playerModel.AbilityType);
            _playerView.Shoot(shell, _playerModel.ShootLaunchForce);            
        }

        private void TakeDamage(int value, IDamagable iD, AbilityType ownerAbility)
        {
            _takeDamageCount++;

            if (_takeDamageCount == _enemies.Length)
            {
                _isShootDelay = false;
                _takeDamageCount = 0;
            }

            var modifier = _damageModifier.GetModifier(ownerAbility, _playerModel.AbilityType);
            _playerModel.Health.TakeDamage(value * modifier);

            UpdateHealthBar();

            Debug.Log($"PlayerHealth {_playerModel.Health.HP}");

            _playerView.OnChangeTurn?.Invoke();
        }

        private void UpdateHealthBar()
        {
            var barValue = (float)_playerModel.Health.HP / _playerModel.Health.MaxHP;
            _playerView.UpdateHealthBar(barValue);
        }

    }
}