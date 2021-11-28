using UnityEngine;

namespace TankGame
{
    public class EnemyFlyingState : IEnemyFlyingState
    {
        public bool IsAlive { get; }
        public bool IsFlying { get; }
        public bool IsReadyToShoot { get; }

        private Transform _enemy;
        private Rigidbody _rigidbody;
        private bool _isFirstUp;

        public EnemyFlyingState()
        {
            IsAlive = true;
            IsFlying = true;
            IsReadyToShoot = false;
        }

        public void EnterState(Transform enemy, Rigidbody rigidbody)
        {
            _enemy = enemy;
            _rigidbody = rigidbody;
            _isFirstUp = true;
        }

        public void ExitState()
        {

        }

        public void Levitate()
        {
            if (_enemy.transform.position.y < 2)
            {
                _rigidbody.AddForce(Vector3.up * 1000, ForceMode.Force);

                if (_isFirstUp) return;

                _rigidbody.AddForce(Vector3.up * 1000, ForceMode.Force);

            }
            else
            {
                _rigidbody.AddForce(Vector3.down * 200, ForceMode.Force);
                _isFirstUp = false;
            }
        }
    }
}