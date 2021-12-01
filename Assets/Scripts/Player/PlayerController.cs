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
        private PlayerModel[] _playersModels;
        private PlayerView[] _playersViews;
        private InputController _inputController;
        private PoolController _poolController;
        private DamageModifier _damageModifier;
        private EndScreenController _endScreenController;
        private AbilitiesManager _abilitiesManager;
        private SkillButtonsManager _skillButtonsManager;
        private bool _isShootDelay;
        private int _activePlayer;

        public PlayerController(PlayerModel[] playersModels, PlayerView[] playersViews, InputController inputController,
                    PoolController poolController, DamageModifier damageModifier, EndScreenController endScreenController, 
                        AbilitiesManager abilitiesManager, TargetProvider targetProvider, SkillButtonsManager skillButtonsManager)
        {
            _playersModels = playersModels;
            _playersViews = playersViews;
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

            for (int i = 0; i < _playersViews.Length; i++)
            {
                _playersViews[i].OnTakeDamage += TakeDamage;
            }
            _endScreenController.OnTakeLife += UpdatePlayerLifes;
        }

        public void CleanUp()
        {
            _inputController.OnClickShootButton -= PlayerShoot;

            for (int i = 0; i < _playersViews.Length; i++)
            {
                _playersViews[i].OnTakeDamage -= TakeDamage;
            }

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
            _playersViews[_activePlayer].Rotate(_targetProvider.GetTarget(), deltatime);            
        }

        private void PlayerShoot()
        {
            if (_isShootDelay) return;

            _isShootDelay = true;

            var shell = _poolController.GetShell();
            var activeButton = _skillButtonsManager.GetActiveSkillButton();
            var ability = _abilitiesManager.GetAbility(activeButton.Button.gameObject);
            _playersModels[_activePlayer].CurrentAbility = ability;
            shell.GetComponent<Shell>().SetDamageValue(_playersModels[_activePlayer].ShootDamageForce, ability.Type);
            _playersViews[_activePlayer].Shoot(shell, _playersModels[_activePlayer].ShootLaunchForce);              
            _playersViews[_activePlayer].StartCoroutine(EndTurn());
            OnShoot?.Invoke();
        }

        private void TakeDamage(int value, IDamagable iD, AbilityType ownerAbility)
        {
            var abilityType = _playersModels[_activePlayer].CurrentAbility.Type;
            var modifier = _damageModifier.GetModifier(ownerAbility, abilityType);
            _playersModels[_activePlayer].Health.TakeDamage(value * modifier);

            if (_playersModels[_activePlayer].Health.HP == 0)
            {                
                _endScreenController.StartLoseScreen(_playersModels[_activePlayer].LifesCount);
            }

            UpdateHealthBar();

            Debug.Log($"PlayerHealth {_playersModels[_activePlayer].Health.HP}");
        }

        private void UpdateHealthBar()
        {
            for (int i = 0; i < _playersModels.Length; i++)
            {
                var barValue = (float)_playersModels[i].Health.HP / _playersModels[i].Health.MaxHP;
                _playersViews[i].UpdateHealthBar(barValue);
            }
        }

        private void UpdatePlayerLifes()
        {
            _playersModels[_activePlayer].LifesCount--;
        }

        private IEnumerator EndTurn()
        {
            yield return new WaitForSeconds(1);
            OnEndTurn?.Invoke();
        }
    }
}