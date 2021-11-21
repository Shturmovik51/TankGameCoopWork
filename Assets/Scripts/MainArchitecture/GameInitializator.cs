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
            var damageModifier = new DamageModifier();

            var playerModel = new PlayerModel(gameData.PlayerModelData);

            var playerFactory = new PlayerFactory(playerModel);
            var playerInitialization = new PlayerInitialization(playerFactory, playerPosition);
            var playerView = playerInitialization.GetPlayer().GetComponent<PlayerView>();
            var playerController = new PlayerController(playerModel, playerView, inputController, poolController, damageModifier);
            var enemyCount = gameData.EnemyModelsData.Length;
            var enemyModels = new EnemyModel[enemyCount];

            var abilitiesManager = new AbilitiesManager(gameData);

            for (int i = 0; i < enemyCount; i++)
            {
                enemyModels[i] = new EnemyModel(gameData.EnemyModelsData[i], abilitiesManager);
            }   

            var enemyFactory = new EnemyFactory(enemyModels);
            var enemyInitialisation = new EnemyInitialization(enemyFactory, enemyPositions);
            var enemyViews = new EnemyView[enemyCount];

            var enemiesPanel = Object.Instantiate(gameData.PrefabsData.EnemiesPanel, canvas.transform); //todo в фабрику
            var playerPanel = Object.Instantiate(gameData.PrefabsData.PlayerPanel, canvas.transform);

            var playerUIFactory = new PlayerUIFactory(gameData, playerPanel);
            var enemyUIFactory = new EnemyUIFactory(gameData, enemiesPanel);

            for (int i = 0; i < enemyCount; i++)
            {
                var enemyPanel = enemyUIFactory.GetEnemyStatsPanel(enemyModels[i], i);
                enemyViews[i] = enemyInitialisation.GetEnemies(i).GetComponent<EnemyView>();
                enemyViews[i].InitStatsPanel(enemyPanel);
            }

            playerView.InitStatsPanel(playerPanel);

            var enemyController = new EnemyController(enemyModels, enemyViews, poolController, playerView, abilitiesManager, damageModifier);
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
