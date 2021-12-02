using UnityEngine;

namespace TankGame
{
    public class TargetController : IInitializable, ICleanable, IController
    {
        private EnemyView[] _enemyViews;
        private EnemyModel[] _enemyModels;
        private TurnController _turnPanelController;
        private GameObject _targetMarker;
        private bool _isOnSearchTarget;
        private int _targetID;
        private PlayerController _playerController;
        private SkillButtonsManager _skillButtonsManager;
        public GameObject TargetMarker => _targetMarker;
        private AbilitiesManager _abilitiesManager;
        public TargetController(EnemyView[] enemyViews, EnemyModel[] enemyModels, TurnController turnPanelController, 
              GameData gameData, SkillButtonsManager skillButtonsManager, AbilitiesManager abilitiesManager, PlayerController playerController)
        {
            _enemyViews = enemyViews;
            _enemyModels = enemyModels;
            _turnPanelController = turnPanelController;
            _targetMarker = gameData.PrefabsData.TargetMarker;
            _skillButtonsManager = skillButtonsManager;
            _abilitiesManager = abilitiesManager;
            _playerController = playerController;
        }

        public void Initialization()
        {
            for (int i = 0; i < _enemyViews.Length; i++)
            {
                _enemyViews[i].OnClickMe += SetOnClickTarget;
                _enemyViews[i].OnPointerEnterMe += SetTempMarker;
                _enemyViews[i].OnPointerExitMe += RemoveTempMarker;
            }

            _turnPanelController.OnSetEnemyTurn += StopSearchTarget;
            _turnPanelController.OnSetPlayerTurn += StartSearchTarget;
            _isOnSearchTarget = true;

            _targetMarker = Object.Instantiate(_targetMarker);
            _targetMarker.SetActive(false);
        }

        public void CleanUp()
        {
            _turnPanelController.OnSetEnemyTurn -= StopSearchTarget;
            _turnPanelController.OnSetPlayerTurn -= StartSearchTarget;
        }

        public GameObject GetTargetMarker()
        {
            return _targetMarker;
        }

        private void SetOnClickTarget(EnemyView enemyView)
        {
            if (!_isOnSearchTarget) return;
            _isOnSearchTarget = false;
            _playerController.StartShootProcedure();
        }

        private void SetTempMarker(EnemyView enemyView)
        {
            if (!_isOnSearchTarget) return;

            var activeskillbutton = _skillButtonsManager.GetActiveSkillButton();
            if (activeskillbutton == null) return;

            var ability = _abilitiesManager.GetAbility(activeskillbutton.Button.gameObject);

            if (ability.Type == AbilityType.FireAbility)
            {
                for (int i = 0; i < _enemyViews.Length; i++)
                {
                    if (_enemyViews[i] == enemyView)
                        _targetID = i;
                }
            }
            else
            {
                for (int i = 0; i < _enemyViews.Length; i++)
                {
                    if (_enemyViews[i] == enemyView && !_enemyModels[i].IsFlying && !_enemyModels[i].IsDead)
                        _targetID = i;
                }
            }

            ResetTargetsStatus();
            ChangeTargetStatus(_targetID);
            SetTargetMarker();
        }

        private void RemoveTempMarker()
        {
            if (!_isOnSearchTarget) return;

            ResetTargetsStatus();
            _targetMarker.SetActive(false);
        }

        private void StartSearchTarget()
        {            
            _isOnSearchTarget = true;
            //ResetTargetsStatus();
            //CurrentTarget();
        }

        private void StopSearchTarget()
        {
            _isOnSearchTarget = false;
            _targetMarker.SetActive(false);
        }        

        private void ResetTargetsStatus()
        {
            foreach (var enemy in _enemyModels)
            {
                enemy.IsTarget = false;
            }
        }

        private void ChangeTargetStatus(int iD)
        {   
            _enemyModels[iD].IsTarget = !_enemyModels[iD].IsTarget;
        }

        private void SetTargetMarker()
        {
            var pos = _targetMarker.transform.position;
            pos.x = _enemyViews[_targetID].transform.position.x;
            pos.z = _enemyViews[_targetID].transform.position.z;
            _targetMarker.transform.position = pos;

            if(!_targetMarker.activeInHierarchy)
                _targetMarker.SetActive(true);
        }
    }
}