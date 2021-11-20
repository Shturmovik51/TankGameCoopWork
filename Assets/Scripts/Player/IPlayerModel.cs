using UnityEngine;

namespace TankGame
{
    public interface IPlayerModel
    {
        public GameObject Tank { get; }
        public int ShootLaunchForce { get; }
        public int ShootDamageForce { get; }
        public Health Health { get; }
        public Ability Ability { get; set; }
    }
}