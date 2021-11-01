using UnityEngine;

namespace TankGame
{
    public class EnemyFactory
    {
        private IEnemyModel[] _enemyModels;

        public EnemyFactory(IEnemyModel[] enemyModels)
        {
            _enemyModels = enemyModels;
        }

        public Transform[] CreateEnemies()
        {
            var transforms = new Transform[_enemyModels.Length];

            for (int i = 0; i < _enemyModels.Length; i++)
            {
                var enemyObject = Object.Instantiate(_enemyModels[i].Tank).transform;
                transforms[i] = enemyObject;
            }

            return transforms;
        }
    }
}