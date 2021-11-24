using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class SkillButtonsCDStateController : IInitializable, ICleanable, IController
    {
        private SkillButtonsManager _skillButtonsManager;
        private TurnController _turnController;
        private PlayerController _playerController;
        public SkillButtonsCDStateController(SkillButtonsManager skillButtonsManager, TurnController turnController, 
                    PlayerController playerController)
        {
            _skillButtonsManager = skillButtonsManager;
            _turnController = turnController;
            _playerController = playerController;
        }

        public void Initialization()
        {
            _turnController.OnSetPlayerTurn += UpdateButtonsOnCD;
            _playerController.OnShoot += ShowButtonCDState;
        }

        public void CleanUp()
        {
            _turnController.OnSetPlayerTurn -= UpdateButtonsOnCD;
            _playerController.OnShoot -= ShowButtonCDState;
        }

        public void ShowButtonCDState()
        {
            var button = _skillButtonsManager.GetActiveSkillButton();
            if (button == null) return;

            button.IsActive = false;
            button.IsOnCD = true;
            button.CDText.gameObject.SetActive(true);  
            button.Button.interactable = false;
            button.CDText.text = button.CurrentCD.ToString();
        }

        public void UpdateButtonsOnCD()
        {
            var _buttonsOnCD = _skillButtonsManager.GetSkillButtonsOnCD();

            if (_buttonsOnCD.Count != 0)
            {
                for (int i = 0; i < _buttonsOnCD.Count; i++)
                {
                    _buttonsOnCD[i].CurrentCD--;
                    _buttonsOnCD[i].CDText.text = _buttonsOnCD[i].CurrentCD.ToString();

                    if (_buttonsOnCD[i].CurrentCD <= 0)
                    {
                        RemoveCDState(_buttonsOnCD[i]);
                    }
                }
            } 
        }

        private void RemoveCDState(SkillButton skillButton)
        {            
            skillButton.CDText.gameObject.SetActive(false);
            skillButton.Button.interactable = true;
            skillButton.CurrentCD = skillButton.MaxCD;
            skillButton.IsOnCD = false;
        }
    }
}