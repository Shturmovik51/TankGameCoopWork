using System;
using UnityEngine;

namespace TankGame
{
    public class EnemyView : MonoBehaviour, IDamagable
    {
        public Action<int, IDamagable> OnTakeDamage { get; set; }
        public Action<int> OnChangeTurn;
        public event Action OnReadyToShoot;

        private Quaternion _startDirection;
        private Quaternion _targetDirection;

        private bool _isOnRotation;
        private float _lerpProgress;
        private float _rotationTime = 1;
        public bool IsOnRotation => _isOnRotation;

        [SerializeField] private Transform ShellStartPosition;

        public void Shoot(GameObject shell, int shootForce)
        {     
            shell.transform.position = ShellStartPosition.position;
            shell.transform.rotation = transform.rotation;
            shell.SetActive(true);
            var shellRigidBody = shell.GetComponent<Rigidbody>();
            shellRigidBody.velocity = Vector3.zero;
            shellRigidBody.AddForce(ShellStartPosition.forward * shootForce, ForceMode.Impulse);
        }

        public void SetStartRotationParameters(Transform target)
        {
            _startDirection = transform.rotation;
            _targetDirection = Quaternion.LookRotation(target.transform.position - transform.position);
            _isOnRotation = true;
        }

        public void Rotate(float deltaTime)
        {
            if (!_isOnRotation) return;

            _lerpProgress += deltaTime / _rotationTime;
            
            transform.rotation = Quaternion.Lerp(_startDirection, _targetDirection, _lerpProgress);

            if (_lerpProgress > 1)
            {
                _lerpProgress = 0;
                _isOnRotation = false;
                OnReadyToShoot?.Invoke();
            }
        }
    }
}