using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class GameMenuController : IUpdatable, ICleanable, IController
    {
        private GameMenuView _view;

        public GameMenuController(GameMenuView view)
        {
            _view = view;
            //_view.Init();
        }

        public void LocalUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _view.gameObject.SetActive(!_view.gameObject.activeInHierarchy);
                Time.timeScale = _view.gameObject.activeInHierarchy ? 0 : 1;
            }
        }

        public void CleanUp()
        {
            _view.ClearSubscribes();
        }
    }
}