using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class EnemiesStateController: IFixedUpdatable, IController
    {
        private List<IEnemyFlyingState> _enemiesFlyingState;
        private List<EnemyStates> _enemiesStates;
        private EnemyModel[] _enemyModels;

        public EnemiesStateController(int enemiesCount, EnemyModel[] enemyModels)
        {
            _enemiesFlyingState = new List<IEnemyFlyingState>(enemiesCount);
            _enemiesStates = new List<EnemyStates>(enemiesCount);
            _enemyModels = enemyModels;

            for (int i = 0; i < enemiesCount; i++)
            {
                var states = new EnemyStates();
                _enemiesStates.Add(states);
                _enemiesFlyingState.Add(states.EnemyGroundState);
            }
        }

        public void LocalFixedUpdate(float fixedDeltatime)
        {
            for (int i = 0; i < _enemiesFlyingState.Count; i++)
            {
                if(_enemiesFlyingState[i] == _enemiesStates[i].EnemyFlyingState)
                {
                    _enemiesFlyingState[i].Levitate();                
                }
            }
        }

        public void SetFlyingState(int iD, Transform transform, Rigidbody rigidbody)
        {
            _enemiesFlyingState[iD].ExitState();

            _enemiesFlyingState[iD] = _enemiesStates[iD].EnemyFlyingState;

            _enemyModels[iD].IsFlying = _enemiesStates[iD].EnemyFlyingState.IsFlying;

            _enemiesFlyingState[iD].EnterState(transform, rigidbody);
        }

        public void SetGroundState(int iD)
        {
            _enemiesFlyingState[iD].ExitState();

            _enemiesFlyingState[iD] = _enemiesStates[iD].EnemyGroundState;

            _enemyModels[iD].IsFlying = _enemiesStates[iD].EnemyGroundState.IsFlying;
        }
    }
}
