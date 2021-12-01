using UnityEngine;

namespace TankGame
{
    public class EnemyGroundState : IEnemyFlyingState
    {
        public bool IsAlive { get; }
        public bool IsFlying { get; }
        public bool IsReadyToShoot { get; }

        public EnemyGroundState()
        {
            IsAlive = true;
            IsFlying = false;
            IsReadyToShoot = true;
        }

        public void EnterState(Transform enemy, Rigidbody rigidbody, BoxCollider tankCollider)
        {
            
        }

        public void Levitate()
        {

        }

        public void ExitState()
        {
            
        }
    }
}