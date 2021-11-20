using UnityEngine;

namespace TankGame
{
    public class PlayerModel : IPlayerModel
    {
        public GameObject Tank { get; }
        public int ShootLaunchForce { get; }
        public int ShootDamageForce { get; }
        public Health Health { get; }
        public Ability Ability { get; set; }
        public AbilityType AbilityType { get; set; }
        public bool IsAbilityActive { get; set; }
        public PlayerModel(PlayerModelData playerModelData)
        {
            Tank = playerModelData.TankPrefab;
            ShootLaunchForce = playerModelData.ShootLaunchForce;
            ShootDamageForce = playerModelData.ShootDamageForce;
            Health = new Health(playerModelData.Health);
        }
    }
}