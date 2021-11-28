using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public interface IBase
    {
        public void CreateAbility();
        public void RemoveAbility();
        public void NextItem();
        public void PrevItem();

    }
}