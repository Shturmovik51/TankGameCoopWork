using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    [CreateAssetMenu(menuName = "DataBase/PlayerModelData", fileName = nameof(PlayerModelData))]
    public class PlayerModelData : ScriptableObject
    {
        [SerializeField] private GameObject _tankPrefab;

        public GameObject TankPrefab => _tankPrefab;
    }
}