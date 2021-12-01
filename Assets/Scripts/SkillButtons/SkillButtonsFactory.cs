using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame
{
    public class SkillButtonsFactory
    {
        private Dictionary<GameObject, Ability> _abilities;
        private List<AbilityModelData> _abilitiesSet;
        private GameObject _skillButtonPref;
        private GameObject[] _playersPanels;
        private StartGameParametersManager _startGameParametersManager;
        public SkillButtonsFactory(GameData gameData, GameObject[] playersPanels, AbilitiesManager abilitiesManager,
                    StartGameParametersManager startGameParametersManager)
        {
            _abilities = abilitiesManager.Abilities;
            _abilitiesSet = gameData.AbilityBase.AbilitySamples;
            _skillButtonPref = gameData.PrefabsData.SkillButton;
            
            _startGameParametersManager = startGameParametersManager;
            _playersPanels = playersPanels;
        }

        public List<SkillButton> GetSkillButtons()
        {
            var skillButtons = new List<SkillButton>(_abilitiesSet.Count);

            for (int i = 0; i < _playersPanels.Length; i++)
            {
                var skillButtonsParent = _playersPanels[i].GetComponentInChildren<HorizontalLayoutGroup>().transform;

                for (int j = 0; j < _abilitiesSet.Count; j++)
                {                    
                    var button = Object.Instantiate(_skillButtonPref).GetComponent<Button>(); 
                    
                    var ability = new Ability(_abilitiesSet[j].Type, _abilitiesSet[j].CD, _abilitiesSet[j].ElementIcon, 
                                                _abilitiesSet[j].DeathIcon, _abilitiesSet[j].ID);

                    button.transform.parent = skillButtonsParent;                   

                    var skillButton = new SkillButton(button, ability);

                    _abilities.Add(button.gameObject, ability);

                    if (_startGameParametersManager.SavedData != null)
                    {
                       // skillButton.IsOnCD = _startGameParametersManager.SavedData.SkillButtonsSave[i].IsOnCD;
                       // skillButton.CurrentCD = _startGameParametersManager.SavedData.SkillButtonsSave[i].CurrentCD;
                    }

                    skillButtons.Add(skillButton);
                }              
            }

            return skillButtons;
        }
    }
}