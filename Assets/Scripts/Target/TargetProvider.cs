using System;
using UnityEngine;

namespace TankGame
{
    public class TargetProvider
    {  
        private EnemyModel[] _enemyModels;
        private EnemyView[] _enemyViews;
        private Transform _currentTarget;

        public TargetProvider(EnemyModel[] enemyModels, EnemyView[] enemyViews)
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
    }
}