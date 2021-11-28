using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public interface IEnemyFlyingState
    {
        public bool IsAlive { get; }
        public bool IsFlying { get; }
        public bool IsReadyToShoot { get; }

        public void EnterState(Transform enemy, Rigidbody rigidbody);
        public void Levitate();
        public void ExitState();
    }
}