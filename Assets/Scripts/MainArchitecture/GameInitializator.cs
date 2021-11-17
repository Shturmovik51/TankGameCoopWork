using UnityEngine;

namespace TankGame
{
    public sealed class GameInitializator
    {
        public GameInitializator(ControllersManager controllersManager, GameData gameData, int effectsCount,
                    GameManager gameManager, Transform playerPosition, Transform[] enemyPositions, UIFields uIFields, 
                        Transform enemyStatsPanelParent)
        {
            var inputController = new InputController(gameData);
            var poolController = new PoolController(gameData, effectsCount, gameManager);

            var playerModel = new PlayerModel(gameData.PlayerModelData);
            var playerFactory = new PlayerFactory(playerModel);
            var playerInitialization = new PlayerInitialization(playerFactory, playerPosition);
            var playerView = playerInitialization.GetPlayer().GetComponent<PlayerView>();
            var playerController = new PlayerController(playerModel, playerView, inputController, poolController);
            var enemyCount = gameData.EnemyModelsData.Length;
            var enemyModels = new EnemyModel[enemyCount];

            for (int i = 0; i < enemyCount; i++)
            {
                enemyModels[i] = new EnemyModel(gameData.EnemyModelsData[i]);
            }   

            var enemyFactory = new EnemyFactory(enemyModels);
            var enemyInitialisation = new EnemyInitialization(enemyFactory, enemyPositions);
            var enemyViews = new EnemyView[enemyCount];

            var entityPanelFactory = new EntityStatsPanelFactory(gameData.PrefabsData.EnemyStatsPanel, enemyStatsPanelParent);
            var abilitiesFactory = new AbilitiesFactory(gameData);

            for (int i = 0; i < enemyCount; i++)
            {
                enemyViews[i] = enemyInitialisation.GetEnemies(i).GetComponent<EnemyView>();
                enemyViews[i].InitStatsPanel(entityPanelFactory.GetEntityStatsPanel(), i + 1);
            } 

            var enemyController = new EnemyController(enemyModels, enemyViews, poolController, playerView, abilitiesFactory);
            var turnController = new TurnController(uIFields, enemyViews, playerView);
            var targetController = new TargetController(enemyViews, enemyModels, turnController, gameData, inputController);
            var targetprovider = new TargetProvider(enemyModels, enemyViews);

            playerController.TargetProvider = targetprovider;
            playerController._enemies = enemyViews;

            controllersManager.Add(inputController);
            controllersManager.Add(poolController); 
            controllersManager.Add(playerController);
            controllersManager.Add(enemyController);
            controllersManager.Add(turnController);
            controllersManager.Add(targetController);
        }
    }
}
