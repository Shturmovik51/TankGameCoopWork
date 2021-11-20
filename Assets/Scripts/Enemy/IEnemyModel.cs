using UnityEngine;

namespace TankGame
{
    public interface IEnemyModel
    {
        public GameObject Tank { get; }
        public Health Health { get; }
        public int ShootLaunchForce { get; }
        public int ShootDamageForce { get; }
    }
}