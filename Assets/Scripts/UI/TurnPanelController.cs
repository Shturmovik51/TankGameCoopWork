using TMPro;

namespace TankGame
{
    public class TurnPanelController: IInitializable, ICleanable, IController
    {
        private TextMeshProUGUI _turnText;
        private EnemyView _enemyView;
        private PlayerView _playerView;
        public TurnPanelController(UIFields uIFields, EnemyView enemyView, PlayerView playerView)
        {
            _turnText = uIFields.TurnText;
            _enemyView = enemyView;
            _playerView = playerView;
        }

        public void Initialization()
        {
            _enemyView.OnChangeTurn += SetEnemyTurn;
            _playerView.OnChangeTurn += SetPlayerTurn;

            SetPlayerTurn();
        }

        public void CleanUp()
        {
            _enemyView.OnChangeTurn -= SetEnemyTurn;
            _playerView.OnChangeTurn -= SetPlayerTurn;
        }

        private void SetPlayerTurn()
        {
            _turnText.text = Strings.PlayerTurn;
        }

        private void SetEnemyTurn()
        {
            _turnText.text = Strings.EnemyTurn;
        }

    }
}