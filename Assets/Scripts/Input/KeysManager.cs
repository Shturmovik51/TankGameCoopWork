using System;
using UnityEngine;

namespace TankGame
{
    public sealed class KeysManager
    {
        public void GetKeyShoot(InputKeysData _inputKeysData, Action action)
        {
            if (Input.GetKey(_inputKeysData.Shoot)) action?.Invoke();
        }

        public void GetKeyNexTarget(InputKeysData _inputKeysData, Action action)
        {
            if (Input.GetKeyDown(_inputKeysData.NextTarget)) action?.Invoke();
        }
        public void GetKeyPreviousTarget(InputKeysData _inputKeysData, Action action)
        {
            if (Input.GetKeyDown(_inputKeysData.PreviousTarget)) action?.Invoke();
        }
    }
}
