using UnityEngine;

namespace TankGame
{
    public interface IAbility
    {
        public Sprite Icon { get; }
        public AbilityType Type { get; }
        public int CD { get; }
        public int ID { get; }
    }
}
