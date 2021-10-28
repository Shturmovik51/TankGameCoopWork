using System.Collections;
using UnityEngine;

namespace TankGame
{
    public class EnemyController: IInitializable, ICleanable, IUpdatable, IController
    {
        private EnemyModel _enemyModel;
        private EnemyView _enemyView;
        private PoolController _poolController;

        public EnemyController(EnemyModel enemyModel, EnemyView enemyView, PoolController poolController)
        {
            _enemyModel = enemyModel;
            _enemyView = enemyView;
            _poolController = poolController;
        }

        public void Initialization()
        {            
            _enemyView.OnTakeDamage += TakeDamage;
        }

        public void CleanUp()
        {
            _enemyView.OnTakeDamage -= TakeDamage;
        }

        public void LocalUpdate(float deltaTime)
        {

        }

        private void EnemyShoot()
        { 
            var shell = _poolController.GetShell();
            shell.GetComponent<Shell>().SetDamageValue(_enemyModel.ShootDamageForce);
            _enemyView.Shoot(shell, _enemyModel.ShootLaunchForce);
        }

        private void TakeDamage(int value)
        {
            _enemyModel.Health -= value;
            _enemyView.StartCoroutine(ShootDelay());
            Debug.Log($"PlayerHealth {_enemyModel.Health}");
            _enemyView.OnChangeTurn?.Invoke();
        }

        private IEnumerator ShootDelay()
        {
            yield return new WaitForSeconds(2);
            EnemyShoot();
        }
    }
}