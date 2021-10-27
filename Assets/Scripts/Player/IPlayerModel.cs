using UnityEngine;

namespace TankGame
{
    public interface IPlayerModel
    {
        public GameObject Tank { get; }
        public int ShootForce { get; }
    }
}