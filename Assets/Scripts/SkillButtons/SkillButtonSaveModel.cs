using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    [System.Serializable]
    public class SkillButtonSaveModel
    {
        [SerializeField] private bool _isOnCD;
        [SerializeField] private int _currentCD;

        public bool IsOnCD => _isOnCD;
        public int CurrentCD => _currentCD;

        public SkillButtonSaveModel(bool isOnCD, int currentCD)
        {
            _isOnCD = isOnCD;
            _currentCD = currentCD;
        }
    }
}