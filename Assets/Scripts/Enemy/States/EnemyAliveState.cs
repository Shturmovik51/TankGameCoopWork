using UnityEngine;

namespace TankGame
{
    public class EnemyAliveState
    {
        public bool IsAlive { get; }
        public bool IsFlying { get; }
        public bool IsReadyToShoot { get; }

        public EnemyAliveState()
        {
            IsAlive = true;
            IsFlying = false;
            IsReadyToShoot = true;
        }

        public void EnterState(Transform enemy, Rigidbody rigidbody)
        {
            throw new System.NotImplementedException();
        }

        public void ExitState()
        {
            throw new System.NotImplementedException();
        }
    }
}