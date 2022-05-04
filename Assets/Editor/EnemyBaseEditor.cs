using UnityEngine;
using UnityEditor;

namespace TankGame
{
    [CustomEditor(typeof(EnemyBase))]
    public class EnemyBaseEditor : Editor
    {
        private EnemyBase _enemyBase;

        private void Awake()
        {
            _enemyBase = (EnemyBase)target;
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("New Enemy"))
                _enemyBase.CreateEnemy();
            if (GUILayout.Button("<="))
                _enemyBase.PrevEnemy();
            if (GUILayout.Button("=>"))
                _enemyBase.NextEnemy();
            if (GUILayout.Button("Remove"))
                _enemyBase.RemoveEnemy();
             
            GUILayout.EndHorizontal();
            GUILayout.Label($"Enemies In Base {_enemyBase.EnemySamples.Count}");
            base.OnInspectorGUI();
        }
    }
}
