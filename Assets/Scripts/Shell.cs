using UnityEngine;

namespace TankGame
{
    public class Shell : MonoBehaviour
    {
        private int _damageValue;
        private AbilityType _ownerAbility;
        private const float EXPLOSION_RADIUS = 20;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damagableObject))
            {
                InflictDamage(damagableObject, other.transform);
            }
        }

        public void SetDamageValue(int value, AbilityType ownerAbility)
        {
            _damageValue = value;
            _ownerAbility = ownerAbility;
        }

        private void InflictDamage(IDamagable damagableObject, Transform objectTransform)
        {  
            if (_ownerAbility == AbilityType.WaterAbility)
            {
                var hits = Physics.OverlapSphere(objectTransform.position, EXPLOSION_RADIUS);

                for (int j = 0; j < hits.Length; j++)
                {
                    if (hits[j].gameObject.TryGetComponent(out IDamagable currentObject))
                    {
                        currentObject.OnTakeDamage?.Invoke(_damageValue, currentObject, _ownerAbility);
                    }
                }
            }
            else
            {
                damagableObject.OnTakeDamage?.Invoke(_damageValue, damagableObject, _ownerAbility);
            }   
        }
    }
}