using System;
using System.Collections;
using UnityEngine;

namespace TankGame
{
    public class PlayerController: IInitializable, ICleanable, IUpdatable, IController
    {
        public event Action OnShoot;
        public event Action OnEndTurn;

        private TargetProvider _targetProvider;
        private PlayerModel _playerModel;
        private PlayerView _playerView;
        private InputController _inputController;
        private PoolController _poolController;
        private DamageModifier _damageModifier;
        private EndScreenController _endScreenController;
        private AbilitiesManager _abilitiesManager;
        private SkillButtonsManager _skillButtonsManager;
        private bool _isShootDelay;

        public PlayerController(PlayerModel playerModel, PlayerView playerView, InputController inputController,
                    PoolController poolController, DamageModifier damageModifier, EndScreenController endScreenController, 
                        AbilitiesManager abilitiesManager, TargetProvider targetProvider, SkillButtonsManager skillButtonsManager)
        {
            _playerModel = playerModel;
            _playerView = playerView;
            _inputController = inputController;
            _poolController = poolController;
            _damageModifier = damageModifier;
            _endScreenController = endScreenController;
            _abilitiesManager = abilitiesManager;
            _targetProvider = targetProvider;
            _skillButtonsManager = skillButtonsManager;
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
        
        public void StartPlayerTurn()
        {
            _isShootDelay = false;
        }

        private void RotateToTarget(float deltatime)
        {
            _playerView.Rotate(_targetProvider.GetTarget(), deltatime);            
        }

        private void PlayerShoot()
        {
            if (_isShootDelay) return;

            _isShootDelay = true;

            var shell = _poolController.GetShell();
            var abilityID = _skillButtonsManager.GetActiveSkillButton().AbilityID;
            var abilityType = _abilitiesManager.GetAbility(abilityID).Type;
            shell.GetComponent<Shell>().SetDamageValue(_playerModel.ShootDamageForce, abilityType);
            _playerView.Shoot(shell, _playerModel.ShootLaunchForce);              
            _playerView.StartCoroutine(EndTurn());
            OnShoot?.Invoke();
        }

        private void TakeDamage(int value, IDamagable iD, AbilityType ownerAbility)
        {            
            var abilityType = _abilitiesManager.GetAbility(_playerModel.AbilityID).Type;
            var modifier = _damageModifier.GetModifier(ownerAbility, abilityType);
            _playerModel.Health.TakeDamage(value * modifier);

            if (_playerModel.Health.HP == 0)
            {                
                _endScreenController.StartLoseScreen(_playerModel.LifesCount);
            }

            UpdateHealthBar();

            Debug.Log($"PlayerHealth {_playerModel.Health.HP}");
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