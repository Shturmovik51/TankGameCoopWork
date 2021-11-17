using UnityEngine;

namespace TankGame
{
    public class EnemyModel : IEnemyModel, IEntity
    {
        public GameObject Tank { get; }
        public int ShootLaunchForce { get; }
        public int ShootDamageForce { get; }
        public bool IsTarget { get; set; }
        public int MaxHealth { get; private set; }
        public int Health { get; set; }
        public Ability Ability { get; set; }
        public EnemyModel(EnemyModelData enemyModelData)
        {
            Tank = enemyModelData.TankPrefab;
            ShootLaunchForce = enemyModelData.ShootLaunchForce;
            ShootDamageForce = enemyModelData.ShootDamageForce;
            Health = enemyModelData.Health;
            MaxHealth = enemyModelData.Health;
        }
    }
}