using UnityEngine;

namespace TankGame
{
    public class EnemyInitialization
    {
        private EnemyFactory _enemyFactory;
        private Transform[] _enemies;

        public EnemyInitialization(EnemyFactory enemyFactory, Transform[] enemyPositions)
        {
            _enemyFactory = enemyFactory;
            _enemies = _enemyFactory.CreateEnemies();

            for (int i = 0; i < _enemies.Length; i++)
            {
                _enemies[i].position = enemyPositions[i].position;
                _enemies[i].rotation = enemyPositions[i].rotation;
            }
        }

        public Transform GetEnemies(int iD)
        {            
            return _enemies[iD];
        }
    }
}