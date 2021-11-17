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
        [SerializeField] private GameObject _enemyStatsPanel;
        public GameObject DustTrail => _dustTrail;
        public GameObject ShellExplosion => _shellExplosion;
        public GameObject TankExplosion => _tankExplosion;
        public GameObject Shell => _shell;
        public GameObject TargetMarker => _targetMarker;
        public GameObject EnemyStatsPanel => _enemyStatsPanel;
    }
}
