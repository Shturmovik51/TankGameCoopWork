using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame
{
    public class SkillButtonsFactory
    {
        private Dictionary<GameObject, Ability> _abilities;
        private List<AbilityModelData> _abilitiesSet;
        private GameObject _skillButtonPref;
        private GameObject[] _playersSkillPanels;
        private PlayerModel[] _playersModels;
        private StartGameParametersManager _startGameParametersManager;
        public SkillButtonsFactory(GameData gameData, GameObject[] playersSkillPanels, AbilitiesManager abilitiesManager,
                    StartGameParametersManager startGameParametersManager, PlayerModel[] playersModels)
        {
            _abilities = abilitiesManager.Abilities;
            _abilitiesSet = gameData.AbilityBase.AbilitySamples;
            _skillButtonPref = gameData.PrefabsData.SkillButton;
            _playersModels = playersModels;
            _startGameParametersManager = startGameParametersManager;
            _playersSkillPanels = playersSkillPanels;
        }

        public List<SkillButton> GetSkillButtons()
        {
            var skillButtons = new List<SkillButton>(_abilitiesSet.Count);

            for (int i = 0; i < _playersSkillPanels.Length; i++)
            {
                var skillButtonsParent = _playersSkillPanels[i].GetComponentInChildren<HorizontalLayoutGroup>().transform;
                var currentAbilitiesSet = new List<Ability>();

                for (int j = 0; j < _abilitiesSet.Count; j++)
                {                    
                    var button = Object.Instantiate(_skillButtonPref).GetComponent<Button>(); 
                    
                    var ability = new Ability(_abilitiesSet[j].Type, _abilitiesSet[j].CD, _abilitiesSet[j].ElementIcon, 
                                                _abilitiesSet[j].DeathIcon, _abilitiesSet[j].ID);

                    button.transform.parent = skillButtonsParent;                   

                    var skillButton = new SkillButton(button, ability);

                    _abilities.Add(button.gameObject, ability);

                    if (_startGameParametersManager.SavedData != null)
                    {
                       // skillButton.IsOnCD = _startGameParametersManager.SavedData.SkillButtonsSave[i].IsOnCD;
                       // skillButton.CurrentCD = _startGameParametersManager.SavedData.SkillButtonsSave[i].CurrentCD;
                    }

                    currentAbilitiesSet.Add(ability);
                    skillButtons.Add(skillButton);
                }

                SetRandomStartAbility(currentAbilitiesSet, i);
            }

            return skillButtons;
        }

        public void SetRandomStartAbility(List<Ability> AbilitiesSet, int index)
        {
            _playersModels[index].CurrentAbility = AbilitiesSet[Random.Range(0, AbilitiesSet.Count)];
        }
    }
}