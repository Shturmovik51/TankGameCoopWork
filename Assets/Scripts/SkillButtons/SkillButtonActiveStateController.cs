using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class SkillButtonActiveStateController
    {
        private List<SkillButton> _skillButtons;
        private PlayerModel _playerModel;
        private SkillButton _activeButton;
        public SkillButtonActiveStateController(PlayerUIFactory playerUIFactory, PlayerModel playerModel)
        {
            _skillButtons = playerUIFactory.GetSkillButtons();
            _playerModel = playerModel;

            foreach (var button in _skillButtons)
            {
                button.Button.onClick.AddListener(() => SetActiveState(button));
            }
        }

        public SkillButton GetActiveButton()
        {            
            ResetButtonsState();
            return _activeButton;
        }

        private void SetActiveState(SkillButton button)
        {
            ResetButtonsState();

            button.IsActive = true;
            button.Button.image.color = Color.green;
            _playerModel.AbilityType = button.AbilityType;
            _playerModel.IsAbilityActive = true;

            _activeButton = button;
        }

        public void ResetButtonsState()
        {
            _playerModel.IsAbilityActive = false;

            foreach (var button in _skillButtons)
            {
                button.IsActive = false;
                button.Button.image.color = Color.white;
            }
        }
    }
}