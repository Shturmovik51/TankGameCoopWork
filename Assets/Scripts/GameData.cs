using UnityEngine;

namespace TankGame
{
    [CreateAssetMenu(menuName = "DataBase/GameData", fileName = nameof(GameData))]
    public sealed class GameData : ScriptableObject
    {
        [SerializeField] private InputKeysData _inputKeysData;
        [SerializeField] private PlayerModelData _playerModelData;
        [SerializeField] private EnemyModelData[] _enemyModelsData;
        [SerializeField] private PrefabsData _prefabsData;
        [SerializeField] private AbilityBase _abilityBase;

        public InputKeysData InputKeysData => _inputKeysData;
        public PlayerModelData PlayerModelData => _playerModelData;
        public EnemyModelData[] EnemyModelsData => _enemyModelsData;
        public PrefabsData PrefabsData => _prefabsData;
        public AbilityBase AbilityBase => _abilityBase;
    }
}
