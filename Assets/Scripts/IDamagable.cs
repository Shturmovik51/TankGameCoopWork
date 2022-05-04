using System;

namespace TankGame
{
    public interface IDamagable
    {
        public Action<int, IDamagable, AbilityType> OnTakeDamage { get; set; }
    }
}
