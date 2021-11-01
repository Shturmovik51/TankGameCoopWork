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
        public TurnController(UIFields uIFields, EnemyView[] enemyViews, PlayerView playerView)
        {
            _turnText = uIFields.TurnText;
            _enemyViews = enemyViews;
            _playerView = playerView;
        }

        public void Initialization()
        {
            foreach (var view in _enemyViews)
            {
                view.OnChangeTurn += SetEnemyTurn;
            }
            
            _playerView.OnChangeTurn += SetPlayerTurn;
            _turnText.text = "Player Turn";
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
            if (_curentEnemyTurn == _enemyViews.Length)
            {
                _isEnemyTurn = false;
                _turnText.text = "Player Turn";

                OnSetPlayerTurn?.Invoke();
            }
        }

        private void SetEnemyTurn(int iD)
        {
            if (!_isEnemyTurn)
            {
                OnSetEnemyTurn?.Invoke();
            }

            _curentEnemyTurn = iD + 1;
            _isEnemyTurn = true;
            _turnText.text = $"Enemy {_curentEnemyTurn} Turn";
        }
    }
}