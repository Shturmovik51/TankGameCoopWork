using System;
using System.Collections;
using UnityEngine;

namespace TankGame
{
    public class PlayerController: IInitializable, ICleanable, IUpdatable, IController
    {
        public event Action OnShoot;
        public event Action OnEndTurn;

        private PlayerTargetProvider _targetProvider;
        private PlayerModel[] _playersModels;
        private PlayerView[] _playersViews;
        private InputController _inputController;
        private PoolController _poolController;
        private DamageModifier _damageModifier;
        private EndScreenController _endScreenController;
        private AbilitiesManager _abilitiesManager;
        private SkillButtonsManager _skillButtonsManager;
        private GameObject[] _playersSkillPanels;
        private bool _isShootDelay;
        private int _activePlayer;
        private Transform _currentTarget;

        public int ActivePlayer => _activePlayer;

        public PlayerController(PlayerModel[] playersModels, PlayerView[] playersViews, InputController inputController,
                    PoolController poolController, DamageModifier damageModifier, EndScreenController endScreenController, 
                        AbilitiesManager abilitiesManager, PlayerTargetProvider targetProvider, 
                            SkillButtonsManager skillButtonsManager, GameObject[] playersSkillPanels)
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
            _playersSkillPanels = playersSkillPanels;
        }

        public void Initialization()
        {
            UpdateHealthBar();
            UpdateElement();

            _activePlayer = -1;
            //_inputController.OnClickShootButton += StartShootProcedure;
            _endScreenController.OnTakeLife += UpdatePlayerLifes;

            for (int i = 0; i < _playersViews.Length; i++)
            {
                _playersViews[i].OnTakeDamage += TakeDamage;
                _playersViews[i].OnReadyToShoot += PlayerShoot;
                _playersSkillPanels[i].SetActive(false);
            }
        }

        public void CleanUp()
        {
            //_inputController.OnClickShootButton -= StartShootProcedure;
            _endScreenController.OnTakeLife -= UpdatePlayerLifes;

            for (int i = 0; i < _playersViews.Length; i++)
            {
                _playersViews[i].OnTakeDamage -= TakeDamage;
                _playersViews[i].OnReadyToShoot -= PlayerShoot;
            }
        }

        public void LocalUpdate(float deltaTime)
        {
            RotateToTarget(deltaTime);
        }  
        
        public void StartPlayerTurn()
        {
            _isShootDelay = false;
            _activePlayer++;

            if (_activePlayer >= _playersModels.Length)
            {
                _activePlayer = 0;
            }

            if (_playersModels[_activePlayer].IsDead)
            {
                StartPlayerTurn();
            }
            else
            {
                EnableActivePlayerSkillPanel();
                _skillButtonsManager.SetAllButtonsNotInCDToActive();
                _playersViews[_activePlayer].Marker.gameObject.SetActive(true);
            }
        }

        public void StartShootProcedure()
        {
            var activeButton = _skillButtonsManager.GetActiveSkillButton();

            if (activeButton == null) return;

            var ability = _abilitiesManager.GetAbility(activeButton.Button.gameObject);
            _playersModels[_activePlayer].CurrentAbility = ability;
            UpdateElement();

            if (ability.Type == AbilityType.GroundAbility)
            {
                _currentTarget = _targetProvider.GetRandomTarget();
            }
            else
            {
                _currentTarget = _targetProvider.GetTarget();
            }

            _playersViews[_activePlayer].SetStartRotationParameters(_currentTarget);

            OnShoot?.Invoke();
            _skillButtonsManager.SetAllButtonsNotInCDToInActive();
        }

        private void RotateToTarget(float deltatime)
        {            
            if(_playersViews[_activePlayer].IsOnRotation)
                _playersViews[_activePlayer].Rotate(deltatime);            
        }       

        private void PlayerShoot()
        { 
            if (_isShootDelay) return;

            _isShootDelay = true;

            var shell = _poolController.GetShell();
            var ability = _playersModels[_activePlayer].CurrentAbility;
            shell.GetComponent<Shell>().SetDamageValue(_playersModels[_activePlayer].ShootDamageForce, ability.Type);
            _playersViews[_activePlayer].Shoot(shell, _playersModels[_activePlayer].ShootLaunchForce);              
            _playersViews[_activePlayer].StartCoroutine(EndTurn());           

           // OnShoot?.Invoke();
        }

        private void TakeDamage(int value, IDamagable view, AbilityType ownerAbility)
        {
            for (int i = 0; i < _playersViews.Length; i++)
            {
                if ((IDamagable)_playersViews[i] == view)
                {
                    var abilityType = _playersModels[_activePlayer].CurrentAbility.Type;
                    var modifier = _damageModifier.GetModifier(ownerAbility, abilityType);
                    _playersModels[i].Health.TakeDamage(value * modifier);

                    if (_playersModels[i].Health.HP == 0)
                    {
                        _playersModels[i].IsDead = true;
                        _playersViews[i].Explosion();
                    }

                    if (AllPlayersDead())
                        _endScreenController.StartLoseScreen(_playersModels[i].LifesCount);  //todo неправильный отсчет жизней

                    UpdateHealthBar();

                    Debug.Log($"PlayerHealth {_playersModels[i].Health.HP}");
                }
            }
        }

        private bool AllPlayersDead()
        {
            var alldead = true;
            for (int i = 0; i < _playersModels.Length; i++)
            {
                if (!_playersModels[i].IsDead)
                    alldead = false;
            }
            return alldead;
        }

        private void UpdateHealthBar()
        {
            for (int i = 0; i < _playersModels.Length; i++)
            {
                var barValue = (float)_playersModels[i].Health.HP / _playersModels[i].Health.MaxHP;
                _playersViews[i].UpdateHealthBar(barValue);
            }
        }

        private void UpdateElement()
        {
            for (int i = 0; i < _playersModels.Length; i++)
            {
                if (!_playersModels[i].IsDead)
                {
                    var icon = _playersModels[i].CurrentAbility.ElementIcon;
                    _playersViews[i].UpdateElement(icon);
                }
            }
        }

        private void UpdatePlayerLifes()
        {
            _playersModels[_activePlayer].LifesCount--;
        }

        private IEnumerator EndTurn()
        {
            for (int i = 0; i < _playersViews.Length; i++)
            {
                _playersViews[i].Marker.gameObject.SetActive(false);
            }


            yield return new WaitForSeconds(1);
            OnEndTurn?.Invoke();
        }

        private void EnableActivePlayerSkillPanel()
        {
            for (int i = 0; i < _playersSkillPanels.Length; i++)
            {
                _playersSkillPanels[i].SetActive(false);
            }

            _playersSkillPanels[_activePlayer].SetActive(true);
        }
    }
}