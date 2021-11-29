using System;
using TMPro;
using UnityEngine;

namespace TankGame
{
    public class TurnController: IInitializable, ICleanable, IController
    {
        public event Action OnSetEnemyTurn;
        public event Action OnSetPlayerTurn;

        private TextMeshProUGUI _turnText;
        private EnemyView[] _enemyViews;
        private PlayerView _playerView;
        private bool _isEnemyTurn;
        private int _curentEnemyTurn;
        private PlayerController _playerController;
        private EnemyController _enemyController;
        public TurnController(UIFields uIFields, EnemyView[] enemyViews, PlayerView playerView,
                        PlayerController playerController, EnemyController enemyController)
        {
            _turnText = uIFields.TurnText;
            _enemyViews = enemyViews;
            _playerView = playerView;
            _playerController = playerController;
            _enemyController = enemyController;
        }

        public void Initialization()
        {
            foreach (var view in _enemyViews)
            {
                view.OnChangeTurn += SetEnemyTurn;
            }

            _enemyController.OnEndTurn += SetPlayerTurn;
            _playerController.OnEndTurn += SetEnemyTurn;
            _playerView.OnChangeTurn += SetPlayerTurn;

            // _turnText.text = "Player Turn";
            SetPlayerTurn();
        }

        public void CleanUp()
        {
            foreach (var view in _enemyViews)
            {
                view.OnChangeTurn -= SetEnemyTurn;
            }

            _playerView.OnChangeTurn -= SetPlayerTurn;
        }

        private void SetPlayerTurn()
        {
            OnSetPlayerTurn?.Invoke();
            _playerController.StartPlayerTurn();
        }

        private void SetEnemyTurn()
        {
            OnSetEnemyTurn?.Invoke();
            _enemyController.StartEnemyTurn();
        }

        private void UpdateTurnPanel<T>(T controller)
        {
            if(controller is PlayerController)
                _turnText.text = "Player Turn";
            if(controller is EnemyController)
                _turnText.text = $"Enemy {_enemyController.CurentEnemy + 1} Turn";
        }
    }
}