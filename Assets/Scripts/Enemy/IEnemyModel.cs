using UnityEngine;

namespace TankGame
{
    public interface IEnemyModel
    {
        public GameObject Tank { get; }
        public int ShootLaunchForce { get; }
        public int ShootDamageForce { get; }
        public int Health { get; set; }
    }
}