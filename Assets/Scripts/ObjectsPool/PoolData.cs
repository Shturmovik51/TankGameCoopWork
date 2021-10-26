using UnityEngine;

namespace TankGame
{
    [CreateAssetMenu(menuName = "DataBase/PoolData", fileName = nameof(PoolData))]
    public sealed class PoolData : ScriptableObject
    {
        [SerializeField] private GameObject _dustTrail;
        [SerializeField] private GameObject _shellExplosion;
        [SerializeField] private GameObject _tankExplosion;
        [SerializeField] private GameObject _shell;
        public GameObject DustTrail => _dustTrail;
        public GameObject ShellExplosion => _shellExplosion;
        public GameObject TankExplosion => _tankExplosion;
        public GameObject Shell => _shell;
    }
}
