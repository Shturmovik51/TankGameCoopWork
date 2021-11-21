using UnityEngine;

namespace TankGame
{
    public interface IAbility
    {
        public Sprite ElementIcon { get; }
        public AbilityType Type { get; }
        public int CD { get; }
        public int ID { get; }
    }
}
