using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class SkillButtonsManager
    {
        private List<SkillButton> _skillButtons;

        public SkillButtonsManager(SkillButtonsFactory skillButtonsFactory)
        {
            _skillButtons = skillButtonsFactory.GetSkillButtons();
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
    }
}