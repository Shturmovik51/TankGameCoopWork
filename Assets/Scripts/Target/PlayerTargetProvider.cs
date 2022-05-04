using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class PlayerTargetProvider
    {
        private EnemyModel[] _enemyModels;
        private EnemyView[] _enemyViews;
        private Transform _currentTarget;

        public PlayerTargetProvider(EnemyModel[] enemyModels, EnemyView[] enemyViews)
        {
            _enemyModels = enemyModels;
            _enemyViews = enemyViews;
        }
        public Transform GetTarget()
        {     
            for (int i = 0; i < _enemyModels.Length; i++)
            {
                if (_enemyModels[i].IsTarget)
                    _currentTarget = _enemyViews[i].transform;                
            }

            if (_currentTarget == null)
                throw new System.Exception("Нет Таргета");
            else
                return _currentTarget;
        }

        public Transform GetRandomTarget()
        {
            var targets = new List<Transform>();

            for (int i = 0; i < _enemyModels.Length; i++)
            {
                if (!_enemyModels[i].IsDead & !_enemyModels[i].IsFlying)
                    targets.Add(_enemyViews[i].transform);
            }

            var index = Random.Range(0, targets.Count);
            return targets[index].transform;
        }
    }
}