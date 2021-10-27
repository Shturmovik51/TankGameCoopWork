using UnityEngine;

namespace TankGame
{    
    public class Shell : MonoBehaviour
    {
        private int _damageValue;
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out IDamagable damagableObject))
            {
                damagableObject.OnTakeDamage?.Invoke(_damageValue);
            }
        }

        public void SetDamageValue(int value)
        {
            _damageValue = value;
        }
    }
}