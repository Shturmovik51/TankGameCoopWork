using UnityEngine;

namespace TankGame
{
    public class PlayerModel : IPlayerModel
    {
        public GameObject Tank { get; }
        public int ShootLaunchForce { get; }
        public int ShootDamageForce { get; }
        public int Health { get; set; }
        public PlayerModel(PlayerModelData playerModelData)
        {
            Tank = playerModelData.TankPrefab;
            ShootLaunchForce = playerModelData.ShootLaunchForce;
            ShootDamageForce = playerModelData.ShootDamageForce;
            Health = playerModelData.Health;
        }
    }
}