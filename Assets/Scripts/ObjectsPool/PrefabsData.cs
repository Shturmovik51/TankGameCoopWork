using UnityEngine;

namespace TankGame
{
    [CreateAssetMenu(menuName = "DataBase/PrefabsData", fileName = nameof(PrefabsData))]
    public sealed class PrefabsData : ScriptableObject
    {
        [SerializeField] private GameObject _dustTrail;
        [SerializeField] private GameObject _hitEffect;
        [SerializeField] private GameObject _tankExplosion;
        [SerializeField] private GameObject _shell;
        [SerializeField] private GameObject _targetMarker;
        [SerializeField] private GameObject _enemyUI;
        [SerializeField] private GameObject _playerUI;
        [SerializeField] private GameObject _enemiesPanel;
        [SerializeField] private GameObject _playerPanel;
        [SerializeField] private GameObject _skillButton;
        [SerializeField] private GameObject _endScreen;
        public GameObject DustTrail => _dustTrail;
        public GameObject HitEffect => _hitEffect;
        public GameObject TankExplosion => _tankExplosion;
        public GameObject Shell => _shell;
        public GameObject TargetMarker => _targetMarker;
        public GameObject EnemyUI => _enemyUI;
        public GameObject PlayerUI => _playerUI;
        public GameObject EnemiesPanel => _enemiesPanel;
        public GameObject PlayerPanel => _playerPanel;
        public GameObject SkillButton => _skillButton;
        public GameObject EndScreen => _endScreen;
    }
}
