using UnityEngine;
using UnityEditor;

namespace TankGame
{
    [CustomEditor(typeof(PlayerBase))]
    public class PlayerBaseEditor : Editor
    {
        private PlayerBase _playerBase;

        private void Awake()
        {
            _playerBase = (PlayerBase)target;
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("New Player"))
                _playerBase.CreatePlayer();
            if (GUILayout.Button("<="))
                _playerBase.PrevPlayer();
            if (GUILayout.Button("=>"))
                _playerBase.NextPlayer();
            if (GUILayout.Button("Remove"))
                _playerBase.RemovePlayer();
             
            GUILayout.EndHorizontal();
            GUILayout.Label($"Players In Base {_playerBase.PlayerSamples.Count}");
            base.OnInspectorGUI();
        }
    }
}
