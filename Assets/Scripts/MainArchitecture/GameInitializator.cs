using UnityEngine;

namespace TankGame
{
    public sealed class GameInitializator
    {
        public GameInitializator(ControllersManager controllersManager, GameData gameData, int effectsCount,
                    GameManager gameManager, Transform playerPosition, Transform[] enemyPositions, UIFields uIFields, 
                        Canvas canvas)
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


            var abilitiesManager = new AbilitiesManager(gameData);

            var enemiesPanel = Object.Instantiate(gameData.PrefabsData.EnemiesPanel, canvas.transform); //todo � �������
            var playerPanel = Object.Instantiate(gameData.PrefabsData.PlayerPanel, canvas.transform);

            var playerUIFactory = new PlayerUIFactory(gameData, playerPanel);


            for (int i = 0; i < enemyCount; i++)
            {
                var enemyPanel = new EnemyUIFactory(gameData, enemiesPanel).GetEntityStatsPanel();
                enemyViews[i] = enemyInitialisation.GetEnemies(i).GetComponent<EnemyView>();
                enemyViews[i].InitStatsPanel(enemyPanel, i + 1);
            }

            playerView.InitStatsPanel(playerPanel);

            var enemyController = new EnemyController(enemyModels, enemyViews, poolController, playerView, abilitiesManager);
            var turnController = new TurnController(uIFields, enemyViews, playerView);
            var skillButtonActiveStateController = new SkillButtonActiveStateController(playerUIFactory, playerModel);
            var skillButtonCDStateController = new SkillButtonCDStateController(skillButtonActiveStateController, turnController, playerController);
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
            controllersManager.Add(skillButtonCDStateController);
        }
    }
}
