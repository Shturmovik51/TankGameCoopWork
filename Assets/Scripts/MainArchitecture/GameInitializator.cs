using UnityEngine;

namespace TankGame
{
    public sealed class GameInitializator
    {
        public GameInitializator(ControllersManager controllersManager, GameData gameData, int effectsCount,
                    GameManager gameManager, Transform playerPosition, Transform[] enemyPositions, UIFields uIFields, 
                        Canvas canvas)
        {
            var abilitiesManager = new AbilitiesManager(gameData);
            var inputController = new InputController(gameData);
            var poolController = new PoolController(gameData, effectsCount, gameManager);
            var damageModifier = new DamageModifier();
            var roundController = Object.FindObjectOfType<RoundController>();

            var endscreen = Object.Instantiate(gameData.PrefabsData.EndScreen, canvas.transform);
            endscreen.SetActive(false);

            var endScreenController = endscreen.GetComponent<EndScreenController>();
            endScreenController.SetRoundController(roundController);

            var playerModel = new PlayerModel(gameData.PlayerModelData, roundController);

            var playerFactory = new PlayerFactory(playerModel);
            var playerInitialization = new PlayerInitialization(playerFactory, playerPosition);
            var playerView = playerInitialization.GetPlayer().GetComponent<PlayerView>();
            
            var enemyCount = gameData.EnemyModelsData.Length;
            var enemyModels = new EnemyModel[enemyCount];


            for (int i = 0; i < enemyCount; i++)
            {
                enemyModels[i] = new EnemyModel(gameData.EnemyModelsData[i], abilitiesManager, roundController);
            }   

            var enemyFactory = new EnemyFactory(enemyModels);
            var enemyInitialisation = new EnemyInitialization(enemyFactory, enemyPositions);
            var enemyViews = new EnemyView[enemyCount];
            var targetprovider = new TargetProvider(enemyModels, enemyViews);

            var enemiesPanel = Object.Instantiate(gameData.PrefabsData.EnemiesPanel, canvas.transform); //todo � �������
            var playerPanel = Object.Instantiate(gameData.PrefabsData.PlayerPanel, canvas.transform);

            var skillButtonsFactory = new SkillButtonsFactory(gameData, playerPanel, abilitiesManager);
            var skillButtonsManager = new SkillButtonsManager(skillButtonsFactory);

            var enemyUIFactory = new EnemyUIFactory(gameData, enemiesPanel, abilitiesManager);

            for (int i = 0; i < enemyCount; i++)
            {
                var enemyPanel = enemyUIFactory.GetEnemyStatsPanel(enemyModels[i], i);
                enemyViews[i] = enemyInitialisation.GetEnemies(i).GetComponent<EnemyView>();
                enemyViews[i].InitStatsPanel(enemyPanel);
            }

            playerView.InitStatsPanel(playerPanel);

            var enemyController = new EnemyController(enemyModels, enemyViews, poolController, playerView, abilitiesManager, 
                                            damageModifier, endScreenController);

            var playerController = new PlayerController(playerModel, playerView, inputController, poolController, damageModifier, 
                                            endScreenController, abilitiesManager, targetprovider, skillButtonsManager);

            var turnController = new TurnController(uIFields, enemyViews, playerView, playerController, enemyController);
            var skillButtonActiveStateController = new SkillButtonsActiveStateController(skillButtonsManager, playerController);
            var skillButtonCDStateController = new SkillButtonsCDStateController(skillButtonsManager, turnController, playerController);
            var targetController = new TargetController(enemyViews, enemyModels, turnController, gameData, inputController);

            endscreen.transform.SetSiblingIndex(5); //todo ��������, ��� ������� ����������

            var saveDataRepository = new SaveDataRepository(inputController, playerModel);



            controllersManager.Add(inputController);
            controllersManager.Add(poolController);
            controllersManager.Add(endScreenController);
            controllersManager.Add(playerController);
            controllersManager.Add(enemyController);
            controllersManager.Add(turnController);
            controllersManager.Add(targetController);
            controllersManager.Add(skillButtonActiveStateController);
            controllersManager.Add(skillButtonCDStateController);
            controllersManager.Add(saveDataRepository);
        }
    }
}
