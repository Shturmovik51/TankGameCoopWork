using System;
using UnityEngine;

namespace TankGame
{
    public sealed class InputController : IUpdatable, IController
    {
        public event Action OnClickShootButton;
        public event Action OnClickNextTarget;
        public event Action OnClickPreviousTarget;

        private readonly KeysManager _inputKeys;
        private readonly InputKeysData _inputKeysData;

        public InputController(GameData gameData)
        {
            _inputKeys = new KeysManager();
            _inputKeysData = gameData.InputKeysData;
        }

        public void LocalUpdate(float deltaTime)
        {
            if (Time.timeScale == Mathf.Round(0)) return;

            _inputKeys.GetKeyShoot(_inputKeysData, OnClickShootButton);
            _inputKeys.GetKeyNexTarget(_inputKeysData, OnClickNextTarget);
            _inputKeys.GetKeyPreviousTarget(_inputKeysData, OnClickPreviousTarget);
        }        
    }
}
