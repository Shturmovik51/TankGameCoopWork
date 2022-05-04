using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class SkillButtonsManager
    {
        private List<SkillButton> _skillButtons;
        private List<SkillButton> _interactableButtons;

        public SkillButtonsManager(SkillButtonsFactory skillButtonsFactory)
        {
            _skillButtons = skillButtonsFactory.GetSkillButtons();
            _interactableButtons = new List<SkillButton>();
        }

        public List<SkillButton> GetSkillButtonsOnCD()
        {
            return _skillButtons.FindAll(skillButton => skillButton.IsOnCD);
        }

        public SkillButton GetActiveSkillButton()
        {
            return _skillButtons.Find(skillButton => skillButton.IsActive);
        }

        public List<SkillButton> GetAllSkillButtons()
        {
            return _skillButtons;
        }

        public void SetAllButtonsNotInCDToInActive()
        {
            _interactableButtons.Clear();

            foreach (var button in _skillButtons)
            {
                if (button.Button.interactable)
                {
                    _interactableButtons.Add(button);
                }
            }

            foreach (var button in _interactableButtons)
            {
                button.Button.interactable = false;
            }
        }

        public void SetAllButtonsNotInCDToActive()
        {
            foreach (var button in _interactableButtons)
            {
                button.Button.interactable = true;
            }
        }
    }
}