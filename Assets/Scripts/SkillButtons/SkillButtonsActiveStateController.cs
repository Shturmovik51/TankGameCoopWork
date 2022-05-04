using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class SkillButtonsActiveStateController : IInitializable, ICleanable, IController
    {

        private List<SkillButton> _skillButtons;
        private TargetMarkerSizeController _targetMarkerSizeController;
        public SkillButtonsActiveStateController(SkillButtonsManager skillButtonsManager, TargetMarkerSizeController targetMarkerSizeController)
        {
            _skillButtons = skillButtonsManager.GetAllSkillButtons();
            _targetMarkerSizeController = targetMarkerSizeController;
        }

        public void Initialization()
        {
            foreach (var skillButton in _skillButtons)
            {
                skillButton.Button.onClick.AddListener(() => SetActiveState(skillButton));
                skillButton.Button.onClick.AddListener(() => _targetMarkerSizeController.ChangeMarkerSize(skillButton));
            }
        }

        public void CleanUp()
        {
            foreach (var skillButton in _skillButtons)
            {
                skillButton.Button.onClick.RemoveAllListeners();
            }
        }

        private void SetActiveState(SkillButton skillButton)
        {
            ResetButtonsState();

            skillButton.IsActive = true;                            //todo сделать стейтконтроллер кнопки
            skillButton.Button.image.color = Color.green;
        }

        public void ResetButtonsState()
        {          
            foreach (var skillButton in _skillButtons)
            {                
                skillButton.IsActive = false;
                skillButton.Button.image.color = Color.white;
            }
        }       
    }
}