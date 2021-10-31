using System;
using TMPro;

namespace TankGame
{
    public class TurnPanelController: IInitializable, ICleanable, IController
    {
        public event Action OnSetEnemyTurn;
        public event Action OnSetPlayerTurn;

        private TextMeshProUGUI _turnText;
        private EnemyView[] _enemyViews;
        private PlayerView _playerView;
        private bool isEnemyTurn;
        public TurnPanelController(UIFields uIFields, EnemyView[] enemyViews, PlayerView playerView)
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

            isEnemyTurn = false;
            _turnText.text = "Player Turn";
        }

        private void SetEnemyTurn(int iD)
        {
            if (!isEnemyTurn)
            {
                OnSetEnemyTurn?.Invoke();
            }

            isEnemyTurn = true;
            _turnText.text = $"Enemy {iD + 1} Turn";
        }
    }
}