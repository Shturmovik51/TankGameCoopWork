using UnityEngine;

namespace TankGame
{
    public class EnemyStates
    {
        public EnemyAliveState EnemyAliveState { get; }
        public EnemyDeadState EnemyDeadState { get; }
        public EnemyFlyingState EnemyFlyingState { get; }
        public EnemyGroundState EnemyGroundState { get; }
        public EnemyReadyToShootState EnemyReadyToShootState { get; }
        public EnemyNotReadyToShootState EnemyNotReadyToShootState { get; }

        public EnemyStates()
        {
            EnemyAliveState = new EnemyAliveState();
            EnemyDeadState = new EnemyDeadState();
            EnemyFlyingState = new EnemyFlyingState();
            EnemyGroundState = new EnemyGroundState();
            EnemyReadyToShootState = new EnemyReadyToShootState();
            EnemyNotReadyToShootState = new EnemyNotReadyToShootState();
        }
    }
}