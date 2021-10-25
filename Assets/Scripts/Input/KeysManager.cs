using System;
using UnityEngine;

namespace TankGame
{
    public sealed class KeysManager
    {
        public void GetKeyShoot(InputKeysData _inputKeysData, Action action)
        {
            if (Input.GetKey(_inputKeysData.Shoot)) action.Invoke();
        }
    }
}
