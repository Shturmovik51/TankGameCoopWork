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
            _playerController.OnShoot += SetCDState;

            SetCDState();
        }

        public void CleanUp()
        {
            _turnController.OnSetPlayerTurn -= UpdateButtonsOnCD;
            _playerController.OnShoot -= SetCDState;
        }

        public void SetCDState()
        {
            var button = _skillButtonsManager.GetActiveSkillButton();
            if (button != null)
            {
                button.IsActive = false;
                button.Button.image.color = Color.white;
                button.IsOnCD = true;
            }

            var buttonsOnCD = _skillButtonsManager.GetSkillButtonsOnCD();
            foreach (var buttonOnCD in buttonsOnCD)
            {
                buttonOnCD.CDText.gameObject.SetActive(true);
                buttonOnCD.Button.interactable = false;
                buttonOnCD.CDText.text = buttonOnCD.CurrentCD.ToString();
            }
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