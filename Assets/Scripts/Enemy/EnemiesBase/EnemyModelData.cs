using UnityEngine;

namespace TankGame
{
    [System.Serializable]
    public class EnemyModelData
    {
        [SerializeField] private GameObject _tankPrefab;
        [SerializeField] private int _shootLaunchForce;
        [SerializeField] private int _shootDamageForce;
        [SerializeField] private int _health;
        public GameObject TankPrefab => _tankPrefab;
        public int ShootLaunchForce => _shootLaunchForce;
        public int ShootDamageForce => _shootDamageForce;
        public int Health => _health;
    }
}