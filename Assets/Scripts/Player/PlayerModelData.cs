using UnityEngine;

namespace TankGame
{
    [CreateAssetMenu(menuName = "DataBase/PlayerModelData", fileName = nameof(PlayerModelData))]
    public class PlayerModelData : ScriptableObject
    {
        [SerializeField] private GameObject _tankPrefab;
        [SerializeField] private int _shootForce;

        public GameObject TankPrefab => _tankPrefab;
        public int ShootForce => _shootForce;
    }
}