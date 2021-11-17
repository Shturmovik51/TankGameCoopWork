using UnityEngine;
using UnityEditor;

namespace TankGame
{
    [CustomEditor(typeof(AbilityBase))]
    public class DataBaseEditor : Editor
    {
        private AbilityBase _abilityBase;

        private void Awake()
        {
            _abilityBase = (AbilityBase)target;
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("New Item"))
            {
                _abilityBase.CreateAbility();
            }

            if (GUILayout.Button("<="))
                _abilityBase.PrevItem();
            if (GUILayout.Button("=>"))
                _abilityBase.NextItem();
            if (GUILayout.Button("Remove"))
                _abilityBase.RemoveAbility();
             

            GUILayout.EndHorizontal();

            GUILayout.Label($"Buffs In Base {_abilityBase.AbilitySamples.Count}");

            base.OnInspectorGUI();
        }
    }
}
