using UnityEngine;

namespace TankGame
{
    public class EnemyReadyToShootState
    {
        public bool IsAlive { get; }
        public bool IsFlying { get; }
        public bool IsReadyToShoot { get; }

        public EnemyReadyToShootState()
        {
            IsAlive = true;
            IsFlying = false;
            IsReadyToShoot = true;
        }

        public void EnterState(Transform enemy, Rigidbody rigidbody)
        {
            
        }

        public void ExitState()
        {
            
        }
    }
}
