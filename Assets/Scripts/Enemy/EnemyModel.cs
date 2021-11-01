using UnityEngine;

namespace TankGame
{
    public class EnemyModel : IEnemyModel
    {
        public GameObject Tank { get; }
        public int ShootLaunchForce { get; }
        public int ShootDamageForce { get; }
        public int Health { get; set; }
        public bool IsTarget { get; set; }
        public EnemyModel(EnemyModelData enemyModelData)
        {
            Tank = enemyModelData.TankPrefab;
            ShootLaunchForce = enemyModelData.ShootLaunchForce;
            ShootDamageForce = enemyModelData.ShootDamageForce;
            Health = enemyModelData.Health;
        }
    }
}