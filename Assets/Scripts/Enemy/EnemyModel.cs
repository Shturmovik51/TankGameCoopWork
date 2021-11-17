using UnityEngine;

namespace TankGame
{
    public class EnemyModel : IEnemyModel, IEntity
    {
        public GameObject Tank { get; }
        public int ShootLaunchForce { get; }
        public int ShootDamageForce { get; }
        public bool IsTarget { get; set; }
        public Health Health { get; }
        public Ability Ability { get; set; }
        public EnemyModel(EnemyModelData enemyModelData)
        {
            Tank = enemyModelData.TankPrefab;
            ShootLaunchForce = enemyModelData.ShootLaunchForce;
            ShootDamageForce = enemyModelData.ShootDamageForce;
            Health = new Health(enemyModelData.Health);
        }
    }
}