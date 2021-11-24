using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame
{
    public class SkillButtonsFactory
    {
        private List<Ability> _abilities;
        private GameObject _skillButtonPref;
        private Transform _skillButtonsParent;
        private StartGameParametersManager _startGameParametersManager;
        public SkillButtonsFactory(GameData gameData, GameObject playerPanel, AbilitiesManager abilitiesManager,
                    StartGameParametersManager startGameParametersManager)
        {
            _abilities = abilitiesManager.Abilities;
            _skillButtonPref = gameData.PrefabsData.SkillButton;
            _skillButtonsParent = playerPanel.GetComponentInChildren<HorizontalLayoutGroup>().transform;
            _startGameParametersManager = startGameParametersManager;
        }

        public List<SkillButton> GetSkillButtons()
        {
            var skillButtons = new List<SkillButton>(_abilities.Count);

            for (int i = 0; i < _abilities.Count; i++)
            {
                var button = Object.Instantiate(_skillButtonPref).GetComponent<Button>();
                button.transform.parent = _skillButtonsParent;
                var skillButton = new SkillButton(button, _abilities[i]);

                if (_startGameParametersManager.SavedData != null)
                {
                    skillButton.IsOnCD = _startGameParametersManager.SavedData.SkillButtonsSave[i].IsOnCD;
                    skillButton.CurrentCD = _startGameParametersManager.SavedData.SkillButtonsSave[i].CurrentCD;
                }

                skillButtons.Add(skillButton);
            }

            return skillButtons;
        }
    }
}