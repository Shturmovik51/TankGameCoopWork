using UnityEngine;

namespace TankGame
{
    public class EnemyModel : IEnemyModel
    {
        public GameObject Tank { get; }
        public int ShootLaunchForce { get; }
        public Health Health { get; }
        public int ShootDamageForce { get; }
        public bool IsTarget { get; set; }
        public bool IsDead { get; set; }
        public bool IsFlying { get; set; }
        public int AbilityID { get; set; }
        public EnemyModel(EnemyModelData enemyModelData, AbilitiesManager abilitiesManager, 
                    StartGameParametersManager startGameParametersManager, int index)
        {
            Tank = enemyModelData.TankPrefab;
            ShootLaunchForce = enemyModelData.ShootLaunchForce;

            if (startGameParametersManager.SavedData == null)
            {
                var shootDamageForse = enemyModelData.ShootDamageForce + (int)(enemyModelData.ShootDamageForce * startGameParametersManager.DifficultIndex);
                ShootDamageForce = shootDamageForse;

                var health = enemyModelData.Health + (int)(enemyModelData.Health * startGameParametersManager.DifficultIndex);
                Health = new Health(health, health);

                AbilityID = abilitiesManager.GetRandomAbilityIndex();

                IsDead = false;
                IsFlying = false;
            }
            else
            {
                ShootDamageForce = startGameParametersManager.SavedData.EnemiesSave[index].ShootDamageForce;
                var maxHealth = startGameParametersManager.SavedData.EnemiesSave[index].MaxHealth;
                var currentHealth = startGameParametersManager.SavedData.EnemiesSave[index].CurrentHealth;
                Health = new Health(maxHealth, currentHealth);

                AbilityID = startGameParametersManager.SavedData.EnemiesSave[index].AbilityID;

                IsDead = startGameParametersManager.SavedData.EnemiesSave[index].IsDead; 
            }
        }
    }
}