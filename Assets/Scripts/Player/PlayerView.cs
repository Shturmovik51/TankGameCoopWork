using System;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame
{
    public class PlayerView : MonoBehaviour, IDamagable
    {
        [SerializeField] private Transform _shellStartPosition;
        [SerializeField] private Transform _tankTower;
        [SerializeField] private Rigidbody _tankRigidbody;
        [SerializeField] private ParticleSystem _explosionBody;
        [SerializeField] private ParticleSystem _explosionTover;
        public Action<int, IDamagable, AbilityType> OnTakeDamage { get; set; }
        public Action OnChangeTurn;
        private int _rotationSpeed = 2;
        private Image _healthBar;

        public void InitStatsPanel(GameObject playerPanel)
        {
            var images = playerPanel.GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].type == Image.Type.Filled)
                    _healthBar = images[i];
            }
        }

        public void Shoot(GameObject shell, int shootForce)
        {
            shell.transform.position = _shellStartPosition.position;
            shell.transform.rotation = transform.rotation;
            shell.SetActive(true);
            var shellRigidBody = shell.GetComponent<Rigidbody>();
            shellRigidBody.velocity = Vector3.zero;
            shellRigidBody.AddForce(_shellStartPosition.forward * shootForce, ForceMode.Impulse);
        }

        public void Rotate(Transform target, float deltaTime)
        {
            var pos = target.position - _tankTower.transform.position;
            var rot = Vector3.RotateTowards(_tankTower.transform.forward, pos, _rotationSpeed * deltaTime, 0.0f);
            _tankTower.transform.rotation = Quaternion.LookRotation(rot);
        }

        public void UpdateHealthBar(float barValue)
        {
            _healthBar.fillAmount = barValue;
        }
    }
}