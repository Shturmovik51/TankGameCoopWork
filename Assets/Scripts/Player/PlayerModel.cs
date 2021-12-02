using UnityEngine;

namespace TankGame
{
    public class PlayerModel : IPlayerModel
    {
        public GameObject Tank { get; }
        public int ShootLaunchForce { get; }
        public int ShootDamageForce { get; }
        public Health Health { get; }
        public int AbilityID { get; set; }
        public int LifesCount { get; set; }
        public Ability CurrentAbility { get; set; }
        public bool IsDead { get; set; }
        public PlayerModel(PlayerModelData playerModelData, StartGameParametersManager startGameParametersManager)
        {
            Tank = playerModelData.TankPrefab;
            ShootLaunchForce = playerModelData.ShootLaunchForce;

            if (startGameParametersManager.SavedData == null)
            {                
                ShootDamageForce = playerModelData.ShootDamageForce;
                Health = new Health(playerModelData.Health, playerModelData.Health);
                LifesCount = startGameParametersManager.LifesCount;
            }
            else
            {
                ShootDamageForce = startGameParametersManager.SavedData.PlayerSave.ShootDamageForce;
                var currentHealth = startGameParametersManager.SavedData.PlayerSave.CurrentHealth;
                var maxHealth = startGameParametersManager.SavedData.PlayerSave.MaxHealth;
                Health = new Health(maxHealth, currentHealth);
                LifesCount = startGameParametersManager.SavedData.PlayerSave.LifesCount;
            }     
        }
    }
}