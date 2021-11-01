using System;

namespace TankGame
{
    public interface IDamagable
    {
        public Action<int, IDamagable> OnTakeDamage { get; set; }
    }
}
