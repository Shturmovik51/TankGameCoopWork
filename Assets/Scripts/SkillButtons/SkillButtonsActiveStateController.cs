using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class SkillButtonsActiveStateController : IInitializable, ICleanable, IController
    {
        private List<SkillButton> _skillButtons;
        PlayerController _playerController;

        public SkillButtonsActiveStateController(SkillButtonsManager skillButtonsManager, PlayerController playerController)
        {
            _skillButtons = skillButtonsManager.GetAllSkillButtons();
            _playerController = playerController;
        }

        public void Initialization()
        {
            _playerController.OnShoot += ResetButtonsState;

            foreach (var skillButton in _skillButtons)
            {
                skillButton.Button.onClick.AddListener(() => SetActiveState(skillButton));
            }
        }

        public void CleanUp()
        {
            _playerController.OnShoot -= ResetButtonsState;

            foreach (var skillButton in _skillButtons)
            {
                skillButton.Button.onClick.RemoveAllListeners();
            }
        }

        private void SetActiveState(SkillButton skillButton)
        {
            ResetButtonsState();

            skillButton.IsActive = true;
            skillButton.Button.image.color = Color.green;
        }

        public void ResetButtonsState()
        {          
            foreach (var skillButton in _skillButtons)
            {                
                //skillButton.IsActive = false;
                skillButton.Button.image.color = Color.white;
            }
        }       
    }
}