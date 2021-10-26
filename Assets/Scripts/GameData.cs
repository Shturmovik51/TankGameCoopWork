using UnityEngine;

namespace TankGame
{
    [CreateAssetMenu(menuName = "DataBase/GameData", fileName = nameof(GameData))]
    public sealed class GameData : ScriptableObject
    {
        [SerializeField] private InputKeysData _inputKeysData;
        [SerializeField] private PlayerModelData _playerModelData;
        [SerializeField] private PoolData _effectsData;

        public InputKeysData InputKeysData => _inputKeysData;
        public PlayerModelData PlayerModelData => _playerModelData;
        public PoolData EffectsData => _effectsData;        
    }
}
