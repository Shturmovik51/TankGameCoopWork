using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame
{
    [System.Serializable]
    public class SkillButtonsParameters
    {
        [SerializeField] AbilityType _elementType;
        [SerializeField, TextArea(1,2)] private string _description;
        [SerializeField] private int _coolDown;

        public AbilityType ElementType => _elementType;
        public string Description => _description;
        public int CoolDown => _coolDown;
    }
}