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
        public bool IsDead { get; set; }
        public Ability Ability { get; set; }
        public EnemyModel(EnemyModelData enemyModelData, AbilitiesManager abilitiesManager, RoundController roundController)
        {
            Tank = enemyModelData.TankPrefab;
            ShootLaunchForce = enemyModelData.ShootLaunchForce;

            var shootDamageForse = enemyModelData.ShootDamageForce + (int)(enemyModelData.ShootDamageForce * roundController.DifficultIndex);
            ShootDamageForce = shootDamageForse;
                        
            var health = enemyModelData.Health + (int)(enemyModelData.Health * roundController.DifficultIndex);
            Health = new Health(health);

            Ability = abilitiesManager.GetRandomAbility();

            IsDead = false;
        }
    }
}