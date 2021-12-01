using System;
using UnityEngine;

namespace TankGame
{
    public class Shell : MonoBehaviour
    {
        public event Action<Transform> OnHit;

        [SerializeField] private ParticleSystem _shellTraser;
        [SerializeField] private Rigidbody _shellRigidbody;
        [SerializeField] private GameObject _shell;
        [SerializeField] private CapsuleCollider _shellCollider;
        [SerializeField] private Light _shellLight;

        private PoolController _poolController;
        private int _damageValue;
        private AbilityType _ownerAbility;
        private const float EXPLOSION_RADIUS = 6;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damagableObject))
            {
                InflictDamage(damagableObject, other.transform);                
            }
        }

        public void InitWithPool()
        {

        }

        public void SetDamageValue(int value, AbilityType ownerAbility)
        {
            _damageValue = value;
            _ownerAbility = ownerAbility;
        }               

        public void SetIdleState()
        {
            _shell.SetActive(true);
            _shellRigidbody.velocity = Vector3.zero;
            _shellRigidbody.isKinematic = false;
            _shellCollider.enabled = true;
            //_shellTraser.Play();
            _shellLight.enabled = true;
        }


        private void InflictDamage(IDamagable damagableObject, Transform objectTransform)
        {  
            if (_ownerAbility == AbilityType.WaterAbility)
            {
                var hits = Physics.OverlapSphere(objectTransform.position, EXPLOSION_RADIUS);
                OnHit?.Invoke(transform);

                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].gameObject.TryGetComponent(out IDamagable currentObject))
                    {
                        SetHitState(currentObject);
                        OnHit?.Invoke(hits[i].transform);
                    }
                }
            }
            else
            {
                SetHitState(damagableObject);
                OnHit?.Invoke(transform);
            }   
        }

        private void SetHitState(IDamagable target)
        {
            target.OnTakeDamage?.Invoke(_damageValue, target, _ownerAbility);
            _shell.SetActive(false);
            _shellRigidbody.velocity = Vector3.zero;
            _shellRigidbody.isKinematic = true;
            _shellCollider.enabled = false;
            _shellTraser.Stop();
            _shellLight.enabled = false;

            
        }
    }
}