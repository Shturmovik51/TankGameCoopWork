using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    [System.Serializable]
    public class PlayerModelForSave
    {
        [SerializeField] private int _shootDamageForce;
        [SerializeField] private int _currentHealth;
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _lifesCount;
        public int ShootDamageForce => _shootDamageForce;
        public int CurrentHealth => _currentHealth;
        public int MaxHealth => _maxHealth;
        public int LifesCount => _lifesCount;
        public PlayerModelForSave(int shootDamageForce, int currentHealth, int maxHealth, int lifesCount)
        {
            _shootDamageForce = shootDamageForce;
            _currentHealth = currentHealth;
            _maxHealth = maxHealth;
            _lifesCount = lifesCount;
        }
    }
}