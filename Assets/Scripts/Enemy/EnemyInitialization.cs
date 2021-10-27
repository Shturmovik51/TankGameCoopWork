using UnityEngine;

namespace TankGame
{
    public class EnemyInitialization
    {
        private EnemyFactory _enemyFactory;
        private Transform _enemy;

        public EnemyInitialization(EnemyFactory enemyFactory, Transform enemyPosition)
        {
            _enemyFactory = enemyFactory;
            _enemy = _enemyFactory.CreateEnemy();
            _enemy.position = enemyPosition.position;
            _enemy.rotation = enemyPosition.rotation;
        }

        public Transform GetEnemy()
        {            
            return _enemy;
        }
    }
}