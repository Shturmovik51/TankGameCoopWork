using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame
{
    public class PlayerUIFactory
    {
        private List<Ability> _abilities;
        private GameObject _skillButtonPref;
        private Transform _skillButtonsParent;

        public PlayerUIFactory(GameData gameData, GameObject playerPanel)
        {
            _abilities = gameData.AbilityBase.AbilitySamples;
            _skillButtonPref = gameData.PrefabsData.SkillButton;
            _skillButtonsParent = playerPanel.GetComponentInChildren<HorizontalLayoutGroup>().transform;
        }

        public List<SkillButton> GetSkillButtons()
        {
            var skillButtons = new List<SkillButton>(_abilities.Count);

            for (int i = 0; i < _abilities.Count; i++)
            {
                var button = Object.Instantiate(_skillButtonPref).GetComponent<Button>();
                button.transform.parent = _skillButtonsParent;
                var skillButton = new SkillButton(button, _abilities[i].CD, _abilities[i].Type, _abilities[i].Icon);
                
                skillButtons.Add(skillButton);
            }

            return skillButtons;
        }
    }
}