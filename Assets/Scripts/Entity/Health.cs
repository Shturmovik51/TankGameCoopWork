using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class Health
    {
        public int MaxHP { get; private set; }
        public int HP { get; private set; }

        public Health(int healthValue)
        {
            HP = healthValue;
            MaxHP = healthValue;
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;

            if (HP <= 0)
                HP = 0;
        }
    }
}