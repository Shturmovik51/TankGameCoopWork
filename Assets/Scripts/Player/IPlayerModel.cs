using UnityEngine;

namespace TankGame
{
    public interface IPlayerModel
    {
        public GameObject Tank { get; }
        public int ShootLaunchForce { get; }
        public int ShootDamageForce { get; }
        public int Health { get; set; }
    }
}