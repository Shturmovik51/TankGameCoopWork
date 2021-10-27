using UnityEngine;

namespace TankGame
{
    public class EnemyFactory
    {
        private IEnemyModel _enemyModel;

        public EnemyFactory(IEnemyModel enemyModel)
        {
            _enemyModel = enemyModel;
        }

        public Transform CreateEnemy()
        {
            var enemyObject = Object.Instantiate(_enemyModel.Tank);

            return enemyObject.transform;
        }
    }
}