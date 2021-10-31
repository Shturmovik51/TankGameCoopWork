using UnityEngine;

namespace TankGame
{
    public class TargetController : IInitializable, IUpdatable, ICleanable, IController
    {
        private EnemyView[] _enemyViews;
        private EnemyModel[] _enemyModels;
        private TurnPanelController _turnPanelController;
        private GameObject _targetMarker;
        private InputController _inputController;
        private bool _isOnSearchTarget;
        private int _targetID;
        public TargetController(EnemyView[] enemyViews, EnemyModel[] enemyModels, TurnPanelController turnPanelController, 
                                    GameData gameData, InputController inputController)
        {
            _enemyViews = enemyViews;
            _enemyModels = enemyModels;
            _turnPanelController = turnPanelController;
            _targetMarker = gameData.PrefabsData.TargetMarker;
            _inputController = inputController;

        }

        public void Initialization()
        {
            _turnPanelController.OnSetEnemyTurn += StartSearchTarget;
            _turnPanelController.OnSetPlayerTurn += StopSearchTarget;
            _inputController.OnClickNextTarget += NextTarget;
            _inputController.OnClickPreviousTarget += PreviousTarget;
            _isOnSearchTarget = true;

            _targetMarker = Object.Instantiate(_targetMarker);
            _targetMarker.SetActive(false);

            SetTargetMarker();
        }

        public void CleanUp()
        {
            _turnPanelController.OnSetEnemyTurn -= StopSearchTarget;
            _turnPanelController.OnSetPlayerTurn -= StartSearchTarget;
            _inputController.OnClickNextTarget -= NextTarget;
            _inputController.OnClickPreviousTarget -= PreviousTarget;
        }

        public void LocalUpdate(float deltaTime)
        {
            if (_isOnSearchTarget)
            {

            }
        }

        private void StartSearchTarget()
        {
            _isOnSearchTarget = true;
            SetTargetMarker();
        }

        private void StopSearchTarget()
        {
            _isOnSearchTarget = false;
            _targetMarker.SetActive(false);
        }

        private void NextTarget()
        {
            _enemyModels[_targetID].IsTarget = false;
            _targetID++;

            if (_targetID > _enemyModels.Length - 1)
                _targetID = 0;

            _enemyModels[_targetID].IsTarget = true;

            SetTargetMarker();
        }

        private void PreviousTarget()
        {
            _enemyModels[_targetID].IsTarget = false;
            _targetID--;

            if (_targetID < 0)
                _targetID = _enemyModels.Length - 1;

            _enemyModels[_targetID].IsTarget = true;

            SetTargetMarker();
        }

        private void SetTargetMarker()
        {
            _targetMarker.transform.position = _enemyViews[_targetID].transform.position;

            if(!_targetMarker.activeInHierarchy)
                _targetMarker.SetActive(true);
        }
    }
}