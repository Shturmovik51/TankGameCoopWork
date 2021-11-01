using System;
using UnityEngine;

namespace TankGame
{
    public class PlayerView : MonoBehaviour, IDamagable
    {
        public Action<int, IDamagable> OnTakeDamage { get; set; }
        public Action OnChangeTurn;
        private int _rotationSpeed = 2;

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

        public void Rotate(Transform target, float deltaTime)
        {
            var pos = target.position - transform.position;
            var rot = Vector3.RotateTowards(transform.forward, pos, _rotationSpeed * deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(rot);
        }
    }
}