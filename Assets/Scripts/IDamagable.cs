using System;

namespace TankGame
{
    public interface IDamagable
    {
        public Action<int> OnTakeDamage { get; set; }
    }
}
