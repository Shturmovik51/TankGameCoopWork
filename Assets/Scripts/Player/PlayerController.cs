using System;
using System.Collections;
using UnityEngine;

namespace TankGame
{
    public class PlayerController: IInitializable, ICleanable, IUpdatable, IController
    {
        public event Action OnShoot;
        public event Action OnEndTurn;
        public TargetProvider TargetProvider;
        public EnemyView[] _enemies;

        private PlayerModel _playerModel;
        private PlayerView _playerView;
        private InputController _inputController;
        private PoolController _poolController;
        private bool _isShootDelay;
        //private int _takeDamageCount;
        private DamageModifier _damageModifier;
        private EndScreenController _endScreenController;

        public PlayerController(PlayerModel playerModel, PlayerView playerView, InputController inputController,
                    PoolController poolController, DamageModifier damageModifier, EndScreenController endScreenController)
        {
            _playerModel = playerModel;
            _playerView = playerView;
            _inputController = inputController;
            _poolController = poolController;
            _damageModifier = damageModifier;
            _endScreenController = endScreenController;
        }
        public void Initialization()
        {
            UpdateHealthBar();

            _inputController.OnClickShootButton += PlayerShoot;
            _playerView.OnTakeDamage += TakeDamage;
            _endScreenController.OnTakeLife += UpdatePlayerLifes;
        }

        public void CleanUp()
        {
            _inputController.OnClickShootButton -= PlayerShoot;
            _playerView.OnTakeDamage -= TakeDamage;
            _endScreenController.OnTakeLife -= UpdatePlayerLifes;
        }

        public void LocalUpdate(float deltaTime)
        {
            RotateToTarget(deltaTime);
        }  
        
        public void SetPlayerTurn()
        {
            _isShootDelay = false;
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

            _playerView.StartCoroutine(EndTurn());

            var shell = _poolController.GetShell();
            shell.GetComponent<Shell>().SetDamageValue(_playerModel.ShootDamageForce, _playerModel.AbilityType);
            _playerView.Shoot(shell, _playerModel.ShootLaunchForce);            
        }

        private void TakeDamage(int value, IDamagable iD, AbilityType ownerAbility)
        {
            //_takeDamageCount++;

            //if (_takeDamageCount == _enemies.Length)    //todo переделать
            //{
            //    _isShootDelay = false;
            //    _takeDamageCount = 0;
            //}

            var modifier = _damageModifier.GetModifier(ownerAbility, _playerModel.AbilityType);
            _playerModel.Health.TakeDamage(value * modifier);

            if (_playerModel.Health.HP == 0)
            {                
                _endScreenController.StartLoseScreen(_playerModel.LifesCount);
            }

            UpdateHealthBar();

            Debug.Log($"PlayerHealth {_playerModel.Health.HP}");

            //_playerView.OnChangeTurn?.Invoke();
        }

        private void UpdateHealthBar()
        {
            var barValue = (float)_playerModel.Health.HP / _playerModel.Health.MaxHP;
            _playerView.UpdateHealthBar(barValue);
        }

        private void UpdatePlayerLifes()
        {
            _playerModel.LifesCount--;
        }

        private IEnumerator EndTurn()
        {
            yield return new WaitForSeconds(1);
            OnEndTurn?.Invoke();
        }


    }
}