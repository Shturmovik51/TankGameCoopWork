using System.Collections;
using UnityEngine;

namespace TankGame
{
    public class EnemyController: IInitializable, ICleanable, IUpdatable, IController
    {
        private EnemyModel[] _enemyModels;
        private EnemyView[] _enemyViews;
        private PoolController _poolController;

        public EnemyController(EnemyModel[] enemyModels, EnemyView[] enemyViews, PoolController poolController)
        {
            _enemyModels = enemyModels;
            _enemyViews = enemyViews;
            _poolController = poolController;
        }

        public void Initialization()
        {
            foreach (var view in _enemyViews)
            {
                view.OnTakeDamage += TakeDamage;
            }
        }

        public void CleanUp()
        {
            foreach (var view in _enemyViews)
            {
                view.OnTakeDamage -= TakeDamage;
            }
        }

        public void LocalUpdate(float deltaTime)
        {

        }

        public void TargetStatusInvertor(int iD)
        {
            _enemyModels[iD].IsTarget = !_enemyModels[iD].IsTarget;
        }

        private void EnemyShoot(int enemyID)
        { 
            var shell = _poolController.GetShell();
            shell.GetComponent<Shell>().SetDamageValue(_enemyModels[enemyID].ShootDamageForce);
            _enemyViews[enemyID].Shoot(shell, _enemyModels[enemyID].ShootLaunchForce);
        }

        private void TakeDamage(int value, IDamagable view)
        {
            for (int i = 0; i < _enemyViews.Length; i++)
            {
                if((IDamagable)_enemyViews[i] == view)
                {
                    _enemyModels[i].Health -= value;
                    _enemyViews[i].StartCoroutine(ShootDelay(i));
                    Debug.Log($"PlayerHealth {_enemyModels[i].Health}");
                    _enemyViews[i].OnChangeTurn?.Invoke(i);
                }
            }
        }

        private IEnumerator ShootDelay(int enemyID)
        {
            yield return new WaitForSeconds(2);
            EnemyShoot(enemyID);
        }
    }
}