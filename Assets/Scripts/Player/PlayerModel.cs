using UnityEngine;

namespace TankGame
{
    public class PlayerModel : IPlayerModel
    {
        public GameObject Tank { get; }

        public int ShootForce { get; }
        public PlayerModel(PlayerModelData playerModelData)
        {
            Tank = playerModelData.TankPrefab;
            ShootForce = playerModelData.ShootForce;
        }
    }
}