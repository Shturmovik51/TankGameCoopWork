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
        private int _readyEnemiesCount;
        private bool _isRevenge;
        private Transform[] _targetsPositions;
        private DamageModifier _damageModifier;
        private EndScreenController _endScreenController;
        private EnemiesStateController _enemiesStatesController;
        private EnemyTargetProvider _enemyTargetProvider;
        public int CurentEnemy => _curentEnemy;

        public EnemyController(EnemyModel[] enemyModels, EnemyView[] enemyViews, PoolController poolController, 
                    PlayerView[] playersViews, AbilitiesManager abilitiesManager, DamageModifier damageModifier, 
                        EndScreenController endScreenController, EnemiesStateController enemiesStatesController, 
                            EnemyTargetProvider enemyTargetProvider)
        {
            _enemyModels = enemyModels;
            _enemyViews = enemyViews;
            _poolController = poolController;
            _targetsPositions = new Transform[playersViews.Length];
            _abilitiesManager = abilitiesManager;
            _damageModifier = damageModifier;
            _endScreenController = endScreenController;
            _enemiesStatesController = enemiesStatesController;
            _enemyTargetProvider = enemyTargetProvider;

            for (int i = 0; i < playersViews.Length; i++)
            {
                _targetsPositions[i] = playersViews[i].gameObject.transform;
            }

        }

        public void Initialization()
        {
            for (int i = 0; i < _enemyViews.Length; i++)
            {
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
                _enemyViews[_curentEnemy].Rotate(deltaTime);
            }
        }

        public void StartEnemyTurn()
        {
            CheckAllDead();

            _readyEnemiesCount = 0;

            bool isOnFlyingState = false;
            foreach (var enemy in _enemyModels)
            {
                if (enemy.IsFlying)
                    isOnFlyingState = true;
            }

            if (isOnFlyingState)
            {
                _enemyViews[_curentEnemy].StartCoroutine(StartTurnDelay());
                SetEnemiesGroundState();
            }
            else
                RevengeTurn();
        }

        private void RevengeTurn()
        {  
            foreach (var view in _enemyViews)
            {
                view.SetStartRotationParameters(_enemyTargetProvider.GetRandomTarget());
            }

            foreach (var model in _enemyModels)
            {
                if (!model.IsDead && !model.IsFlying)
                {
                    model.IsReadyForTurn = true;
                }
            }
            
            FindReadyToShootEnemy();
            _isRevenge = true;
        }

        private void CheckAllDead()
        {
            var isAllDead = true;

            foreach (var model in _enemyModels)
            {
                if (!model.IsDead)
                    isAllDead = false;
            }

            if (isAllDead)
                _endScreenController.StartWinScreen();
        }

        private void SetEnemiesFlyingState()
        {
            foreach (var model in _enemyModels)
            {
                if (!model.IsDead && !model.IsFlying)
                {
                    _readyEnemiesCount++;
                }
            }

            for (int i = 0; i < _readyEnemiesCount - 1; i++)
            {
                var index = UnityEngine.Random.Range(0, _enemyModels.Length);
                var enemy = _enemyViews[index];

                if (!_enemyModels[index].IsDead && !_enemyModels[index].IsFlying)
                    _enemiesStatesController.SetFlyingState(index, enemy.transform, enemy.TankRigidbody, 
                                                                enemy.TankCollider, enemy.FlyEffect);
            }
        }

        private void SetEnemiesGroundState()
        {
            for (int i = 0; i < _enemyModels.Length; i++)            
            {
                if(_enemyModels[i].IsFlying)
                    _enemiesStatesController.SetGroundState(i);
            }
        }

        private void FindReadyToShootEnemy()
        {
            for (int i = 0; i < _enemyModels.Length; i++)
            {
                if (_enemyModels[i].IsReadyForTurn)
                {
                    _curentEnemy = i;
                    _enemyModels[i].IsReadyForTurn = false;
                    return;
                }
            }

            _isRevenge = false;
            SetEnemiesFlyingState();
            _enemyViews[_curentEnemy].StartCoroutine(EndTurnDelay());
        }

        private void TakeDamage(int value, IDamagable view, AbilityType ownerAbility)
        {
            for (int i = 0; i < _enemyViews.Length; i++)
            {
                if((IDamagable)_enemyViews[i] == view)
                {
                    var abilityType = _abilitiesManager.GetAbility(_enemyViews[i].gameObject).Type;
                    var modifier = _damageModifier.GetModifier(ownerAbility, abilityType);
                    _enemyModels[i].Health.TakeDamage(value * modifier);

                    if (_enemyModels[i].Health.HP == 0)
                    {
                        _enemyModels[i].IsDead = true;
                        _enemyViews[i].Explosion();
                    }

                    var barValue = (float)_enemyModels[i].Health.HP / _enemyModels[i].Health.MaxHP;
                    _enemyViews[i].UpdateHPBar(barValue);
                }
            }
        }        

        private void StartEnemyShootDelay()
        {
            _enemyViews[_curentEnemy].StartCoroutine(ShootDelay());

        }

        private void EnemyShoot(int enemyID)
        { 
            var shell = _poolController.GetShell();
            var shootDamageForce = _enemyModels[enemyID].ShootDamageForce;
            var abilityType = _abilitiesManager.GetAbility(_enemyViews[enemyID].gameObject).Type;
            shell.GetComponent<Shell>().SetDamageValue(shootDamageForce, abilityType);

            _enemyViews[enemyID].Shoot(shell, _enemyModels[enemyID].ShootLaunchForce);

            FindReadyToShootEnemy();      
        }

        private IEnumerator StartTurnDelay()
        {
            yield return new WaitForSeconds(1.5f);
            RevengeTurn();
        }

        private IEnumerator ShootDelay()
        {
            yield return new WaitForSeconds(0.5f);
            EnemyShoot(_curentEnemy);
        }

        private IEnumerator EndTurnDelay()
        {
            yield return new WaitForSeconds(1);
            OnEndTurn?.Invoke();
        }
    }
}