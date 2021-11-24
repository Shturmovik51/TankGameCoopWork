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
        public PlayerModel(PlayerModelData playerModelData, RoundController roundController)
        {
            Tank = playerModelData.TankPrefab;
            ShootLaunchForce = playerModelData.ShootLaunchForce;
            ShootDamageForce = playerModelData.ShootDamageForce;
            Health = new Health(playerModelData.Health);
            LifesCount = roundController.LifesCount;
        }
    }
}