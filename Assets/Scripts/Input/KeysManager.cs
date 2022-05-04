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

        public void GetKeyNexTargetDown(InputKeysData _inputKeysData, Action action)
        {
            if (Input.GetKeyDown(_inputKeysData.NextTarget)) action?.Invoke();
        }

        public void GetKeyPreviousTargetDown(InputKeysData _inputKeysData, Action action)
        {
            if (Input.GetKeyDown(_inputKeysData.PreviousTarget)) action?.Invoke();
        }

        public void GetKeySaveDown(InputKeysData _inputKeysData, Action action)
        {
            if (Input.GetKeyDown(_inputKeysData.Save)) action?.Invoke();
        }

        public void GetKeyLoadDown(InputKeysData _inputKeysData, Action action)
        {
            if (Input.GetKeyDown(_inputKeysData.Load)) action?.Invoke();
        }
    }
}
