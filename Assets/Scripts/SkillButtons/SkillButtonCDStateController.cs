using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class SkillButtonCDStateController : IInitializable, ICleanable, IController
    {
        private SkillButtonActiveStateController _skillButtonActiveStateController;
        private List<SkillButton> _buttonsOnCD;
        private TurnController _turnController;
        private PlayerController _playerController;
        public SkillButtonCDStateController(SkillButtonActiveStateController skillButtonActiveStateController,
                    TurnController turnController, PlayerController playerController)
        {
            _skillButtonActiveStateController = skillButtonActiveStateController;
            _turnController = turnController;
            _playerController = playerController;
            _buttonsOnCD = new List<SkillButton>(4);
        }

        public void Initialization()
        {
            _turnController.OnSetPlayerTurn += UpdateButtonsOnCD;
            _playerController.OnShoot += AddButtonToCDList;
        }

        public void CleanUp()
        {
            _turnController.OnSetPlayerTurn -= UpdateButtonsOnCD;
            _playerController.OnShoot -= AddButtonToCDList;
        }

        public void AddButtonToCDList()
        {
            var button = _skillButtonActiveStateController.GetActiveButton();

            _buttonsOnCD.Add(button);
            button.CDText.gameObject.SetActive(true);
            button.Button.interactable = false;
            UpdateCDStateUI();
        }

        public void UpdateCDStateUI()
        {
            foreach (var button in _buttonsOnCD)
            {
                button.CDText.text = button.CurrentCD.ToString();
            }
        }

        public void UpdateButtonsOnCD()
        {
            if (_buttonsOnCD.Count != 0)
            {
                for (int i = 0; i < _buttonsOnCD.Count; i++)
                {
                    _buttonsOnCD[i].CurrentCD--;
                    if (_buttonsOnCD[i].CurrentCD <= 0)
                    {
                        RemoveCDState(_buttonsOnCD[i]);
                    }
                }
            }

            UpdateCDStateUI();
        }

        private void RemoveCDState(SkillButton skillButton)
        {
            _buttonsOnCD.Remove(skillButton);
            skillButton.CDText.gameObject.SetActive(false);
            skillButton.Button.interactable = true;
            skillButton.CurrentCD = skillButton.MaxCD;
        }
    }
}