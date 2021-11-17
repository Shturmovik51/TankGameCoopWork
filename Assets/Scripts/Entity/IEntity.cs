using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public interface IEntity
    {
        public Ability Ability { get; set; }
        public int MaxHealth { get; }
        public int Health { get; set; }
    }
}