using UnityEngine;

namespace TankGame
{
    public class EnemyNotReadyToShootState
    {
        public bool IsAlive { get; }
        public bool IsFlying { get; }
        public bool IsReadyToShoot { get; }

        public EnemyNotReadyToShootState()
        {
            IsAlive = true;
            IsFlying = false;
            IsReadyToShoot = false;
        }

        public void EnterState(Transform enemy, Rigidbody rigidbody)
        {
           
        }

        public void ExitState()
        {

        }
    }
}
