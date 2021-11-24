using UnityEngine;

namespace TankGame
{
    public interface IPlayerModel
    {
        public GameObject Tank { get; }
        public Health Health { get; }
        public int ShootLaunchForce { get; }
        public int ShootDamageForce { get; }
        public int LifesCount { get; set; }
       // public int AbilityID { get; set; }
    }
}