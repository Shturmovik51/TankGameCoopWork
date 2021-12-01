using UnityEngine;

namespace TankGame
{
    [CreateAssetMenu(menuName = "DataBase/GameData", fileName = nameof(GameData))]
    public sealed class GameData : ScriptableObject
    {
        [SerializeField] private InputKeysData _inputKeysData;
        [SerializeField] private PlayerBase _playerBase;
        [SerializeField] private EnemyBase _enemyBase;
        [SerializeField] private PrefabsData _prefabsData;
        [SerializeField] private AbilityBase _abilityBase;

        public InputKeysData InputKeysData => _inputKeysData;
        public PlayerBase PlayerBase => _playerBase;
        public EnemyBase EnemyBase => _enemyBase;
        public PrefabsData PrefabsData => _prefabsData;
        public AbilityBase AbilityBase => _abilityBase;
    }
}
