using UnityEngine;

namespace TankGame
{
    public class EnemyDeadState
    {
        public bool IsAlive { get; }
        public bool IsFlying { get; }
        public bool IsReadyToShoot { get; }

        public EnemyDeadState()
        {
            IsAlive = false;
            IsFlying = false;
            IsReadyToShoot = false;
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
