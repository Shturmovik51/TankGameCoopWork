using System;
using System.Collections;
using UnityEngine;

namespace TankGame
{
    public class EnemyController: IInitializable, ICleanable, IUpdatable, IController
    {
        public event Action OnEndTurn;
        private EnemyModel[] _enemyModels;
        private EnemyView[] _enemyViews;
        private PoolController _poolController;
        private AbilitiesManager _abilitiesManager;
        private int _curentEnemy;
        private bool _isRevenge;
        private Transform _targetPosition;
        private DamageModifier _damageModifier;
        EndScreenController _endScreenController;
        public int CurentEnemy => _curentEnemy;

        public EnemyController(EnemyModel[] enemyModels, EnemyView[] enemyViews, PoolController poolController, 
                        PlayerView playerView, AbilitiesManager abilitiesManager, DamageModifier damageModifier, 
                                EndScreenController endScreenController)
        {
            _enemyModels = enemyModels;
            _enemyViews = enemyViews;
            _poolController = poolController;
            _targetPosition = playerView.transform;
            _abilitiesManager = abilitiesManager;
            _damageModifier = damageModifier;
            _endScreenController = endScreenController;
        }

        public void Initialization()
        {
            for (int i = 0; i < _enemyViews.Length; i++)
            {
               // _enemyModels[i].Ability = _abilitiesManager.GetRandomAbility();
                var barValue = (float)_enemyModels[i].Health.HP / _enemyModels[i].Health.MaxHP;
                _enemyViews[i].UpdateHPBar(barValue);
                _enemyViews[i].OnTakeDamage += TakeDamage;
                _enemyViews[i].OnReadyToShoot += StartEnemyShootDelay;
            }
        }

        public void CleanUp()
        {
            foreach (var view in _enemyViews)
            {
                view.OnTakeDamage -= TakeDamage;
                view.OnReadyToShoot -= StartEnemyShootDelay;
            }
        }

        public void LocalUpdate(float deltaTime)
        {
            if (_isRevenge)
            {
                CheckEnemyDeath(_curentEnemy);
                _enemyViews[_curentEnemy].Rotate(deltaTime);
            }
        }

        public void TargetStatusInvertor(int iD)
        {
            _enemyModels[iD].IsTarget = !_enemyModels[iD].IsTarget;
        }

        public void RevengeTurn()
        {
            foreach (var view in _enemyViews)
            {
                view.SetStartRotationParameters(_targetPosition);
            }

            CheckEnemyDeath(_curentEnemy);

            _isRevenge = true;


            //_enemyViews[_curentEnemy].OnChangeTurn?.Invoke();
        }

        private void StartEnemyShootDelay()
        {
            _enemyViews[_curentEnemy].StartCoroutine(ShootDelay());

        }

        private void EnemyShoot(int enemyID)
        { 
            var shell = _poolController.GetShell();
            shell.GetComponent<Shell>().SetDamageValue(_enemyModels[enemyID].ShootDamageForce, _enemyModels[enemyID].Ability.Type);
            _enemyViews[enemyID].Shoot(shell, _enemyModels[enemyID].ShootLaunchForce);

            _enemyViews[_curentEnemy].StartCoroutine(TurnDelay());           
        }

        private void TakeDamage(int value, IDamagable view, AbilityType ownerAbility)
        {
            for (int i = 0; i < _enemyViews.Length; i++)
            {
                if((IDamagable)_enemyViews[i] == view)
                {
                    var modifier = _damageModifier.GetModifier(ownerAbility, _enemyModels[i].Ability.Type);
                    _enemyModels[i].Health.TakeDamage(value * modifier);

                    if (_enemyModels[i].Health.HP == 0)
                    {
                        _enemyModels[i].IsDead = true;
                    }

                    var barValue = (float)_enemyModels[i].Health.HP / _enemyModels[i].Health.MaxHP;
                    _enemyViews[i].UpdateHPBar(barValue);
                    _curentEnemy = 0;

                   // CheckEnemyDeath(i);
                    //RevengeTurn();

                    //_enemyViews[_curentEnemy].OnChangeTurn?.Invoke(_curentEnemy);
                }
            }
        }        

        private IEnumerator ShootDelay()
        {
            yield return new WaitForSeconds(1);
            EnemyShoot(_curentEnemy);
        }

        private IEnumerator TurnDelay()
        {
            yield return new WaitForSeconds(1);

            _curentEnemy++;

            if (_curentEnemy > _enemyModels.Length - 1)
            {
                _curentEnemy = 0;
                _isRevenge = false;

                CheckAllDed();

                OnEndTurn?.Invoke();
            }

            //if (_curentEnemy > _enemyModels.Length - 1)
            //{
            //    _curentEnemy = 0;
            //    _isRevenge = false;
            //    OnEndTurn?.Invoke();
            //}
            //else
            //{                
            //    //_enemyViews[_curentEnemy].OnChangeTurn?.Invoke();
            //}
        }

        private void CheckEnemyDeath(int iD)
        {
            if (_enemyModels[iD].IsDead)
                _curentEnemy++;

            if (_curentEnemy > _enemyModels.Length - 1)
            {
                _curentEnemy = 0;
                _isRevenge = false;

                CheckAllDed();

                OnEndTurn?.Invoke();
            }
        }

        private void CheckAllDed()
        {
            EnemyModel alife = null;
            for (int i = 0; i < _enemyModels.Length; i++)
            {
                if (!_enemyModels[i].IsDead)
                    alife = _enemyModels[i];
            }
            if(alife == null)
            {
                _endScreenController.StartWinScreen();
            }
        }
    }
}