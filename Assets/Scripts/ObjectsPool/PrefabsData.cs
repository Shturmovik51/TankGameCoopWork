using UnityEngine;

namespace TankGame
{
    [CreateAssetMenu(menuName = "DataBase/PrefabsData", fileName = nameof(PrefabsData))]
    public sealed class PrefabsData : ScriptableObject
    {
        [SerializeField] private GameObject _dustTrail;
        [SerializeField] private GameObject _shellExplosion;
        [SerializeField] private GameObject _tankExplosion;
        [SerializeField] private GameObject _shell;
        [SerializeField] private GameObject _targetMarker;
        [SerializeField] private GameObject _enemyUI;
        [SerializeField] private GameObject _enemiesPanel;
        [SerializeField] private GameObject _playerPanel;
        [SerializeField] private GameObject _skillButton;
        public GameObject DustTrail => _dustTrail;
        public GameObject ShellExplosion => _shellExplosion;
        public GameObject TankExplosion => _tankExplosion;
        public GameObject Shell => _shell;
        public GameObject TargetMarker => _targetMarker;
        public GameObject EnemyUI => _enemyUI;
        public GameObject EnemiesPanel => _enemiesPanel;
        public GameObject PlayerPanel => _playerPanel;
        public GameObject SkillButton => _skillButton;
    }
}
