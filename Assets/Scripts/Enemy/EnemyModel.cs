using UnityEngine;

namespace TankGame
{
    public class EnemyModel : IEnemyModel, IEntity
    {
        public GameObject Tank { get; }
        public int ShootLaunchForce { get; }
        public Health Health { get; }
        public int ShootDamageForce { get; }
        public bool IsTarget { get; set; }
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