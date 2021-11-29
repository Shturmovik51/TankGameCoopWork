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
        private BoxCollider _tankCollider;
        private float _flyingForce = 1000;
        private float _stabilizingForce = 200;
        private bool _isFirstUp;

        public EnemyFlyingState()
        {
            IsAlive = true;
            IsFlying = true;
            IsReadyToShoot = false;
        }

        public void EnterState(Transform enemy, Rigidbody rigidbody, BoxCollider tankCollider)
        {
            _enemy = enemy;
            _rigidbody = rigidbody;
            _isFirstUp = true;
            _tankCollider = tankCollider;
            _tankCollider.enabled = false;
        }

        public void ExitState()
        {
            _tankCollider.enabled = true;
        }

        public void Levitate()
        {
            if (_enemy.transform.position.y < 2)
            {
                _rigidbody.AddForce(Vector3.up * _flyingForce, ForceMode.Force);

                if (_isFirstUp) return;

                _rigidbody.AddForce(Vector3.up * _flyingForce, ForceMode.Force);
            }
            else
            {
                _rigidbody.AddForce(Vector3.down * _stabilizingForce, ForceMode.Force);
                _isFirstUp = false;
            }
        }
    }
}