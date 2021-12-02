using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class TargetMarkerSizeController
    {
        private GameObject _targetMarker;
        private TargetController _targetController;
        private AbilitiesManager _abilitiesManager;
        private Vector3 normalSize = new Vector3(1f, 1f, 1f);
        private Vector3 bigSize = new Vector3(4, 4, 4);
        public TargetMarkerSizeController(TargetController targetController, AbilitiesManager abilitiesManager)
        {
            _targetController = targetController;
            _targetMarker = targetController.GetTargetMarker();
            _abilitiesManager = abilitiesManager;
        }

        public void ChangeMarkerSize(SkillButton skillButton)
        {
            var ability = _abilitiesManager.GetAbility(skillButton.Button.gameObject).Type;
            if (ability == AbilityType.WaterAbility)
            {
                //_targetMarker.transform.localScale = bigSize;
                _targetController.TargetMarker.transform.localScale = bigSize;
            }
            else
            {
                //_targetMarker.transform.localScale = normalSize;
                _targetController.TargetMarker.transform.localScale = normalSize;
            }
        }
    }
}