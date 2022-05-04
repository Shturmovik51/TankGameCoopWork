using UnityEngine;

namespace TankGame
{
    public class EnemyFactory
    {
        private IEnemyModel[] _enemyModels;
        private GameData _gameData;
        private AbilitiesManager _abilitiesManager;
        public EnemyFactory(IEnemyModel[] enemyModels, GameData gameData, AbilitiesManager abilitiesManager)
        {
            _enemyModels = enemyModels;
            _gameData = gameData;
            _abilitiesManager = abilitiesManager;
        }

        public Transform[] CreateEnemies()
        {
            var transforms = new Transform[_enemyModels.Length];

            for (int i = 0; i < _enemyModels.Length; i++)
            {
                var enemyObject = Object.Instantiate(_enemyModels[i].Tank).transform;
                var abilitySamples = _gameData.AbilityBase.AbilitySamples;
                var randomAbility = abilitySamples[Random.Range(0, abilitySamples.Count)];

                var ability = new Ability(randomAbility.Type, randomAbility.CD, randomAbility.ElementIcon, 
                                            randomAbility.DeathIcon, randomAbility.ID);

                _abilitiesManager.AddAbility(enemyObject.gameObject, ability);

                transforms[i] = enemyObject;
            }

            return transforms;
        }
    }
}