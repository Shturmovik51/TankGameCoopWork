using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public sealed class GameInitializator
    {
        public GameInitializator(ControllersManager controllersManager, GameData gameData, int effectsCount,
                    GameStarter gameManager, Transform[] playersPositions, Transform[] enemiesPositions, UIFields uIFields, 
                        Canvas canvas)
        {
            var abilitiesManager = new AbilitiesManager();
            var inputController = new InputController(gameData);
            var poolController = new PoolController(gameData, effectsCount, gameManager);
            var damageModifier = new DamageModifier();
            var startGameParametersManager = Object.FindObjectOfType<StartGameParametersManager>();
            var unitsUIPositionController = new UnitsUIPositionController();



            var endscreen = Object.Instantiate(gameData.PrefabsData.EndScreen, canvas.transform);
            endscreen.SetActive(false);

            var endScreenController = endscreen.GetComponent<EndScreenController>();
            endScreenController.SetRoundController(startGameParametersManager);

            //var enemiesPanel = Object.Instantiate(gameData.PrefabsData.EnemiesPanel, canvas.transform);
            var playersSkillPanels = new GameObject[playersPositions.Length];
            var playersHPpanels = new GameObject[playersPositions.Length];

            for (int i = 0; i < playersPositions.Length; i++)
            {
                playersSkillPanels[i] = Object.Instantiate(gameData.PrefabsData.PlayerPanel, canvas.transform);
               // playersHPpanels[i] = Object.Instantiate(gameData.PrefabsData.PlayerUI, canvas.transform);
            }
            


            var playerFactory = new PlayerFactory(gameData, startGameParametersManager);
            var playerUIFactory = new PlayerUIFactory(gameData, canvas, abilitiesManager);
            var playerInitialization = new PlayerInitialization(playerFactory, playersPositions);
            var playersModels = playerFactory.GetPlayersModels();
            var playersViews = playerInitialization.GetPlayersViews();

            for (int i = 0; i < playersViews.Length; i++)
            {
                var panel = playerUIFactory.GetPlayerStatsPanel(playersViews[i], i);
                playersViews[i].InitStatsPanel(panel);
                unitsUIPositionController.AddUIElement(playersViews[i].transform, panel.StatsPanel);
            }

            var enemyCount = gameData.EnemyBase.EnemySamples.Count;
            var enemyModels = new EnemyModel[enemyCount];            

            for (int i = 0; i < enemyCount; i++)
            {
                var enemySamples = gameData.EnemyBase.EnemySamples;
                enemyModels[i] = new EnemyModel(enemySamples[i], abilitiesManager, startGameParametersManager, i);                
            }

            var enemiesStatesController = new EnemiesStateController(enemyCount, enemyModels);

            var enemyFactory = new EnemyFactory(enemyModels, gameData, abilitiesManager);
            var enemyInitialisation = new EnemyInitialization(enemyFactory, enemiesPositions);
            var enemyViews = new EnemyView[enemyCount];
            var targetprovider = new TargetProvider(enemyModels, enemyViews);


            var skillButtonsFactory = new SkillButtonsFactory(gameData, playersSkillPanels, abilitiesManager, startGameParametersManager);
            var skillButtonsManager = new SkillButtonsManager(skillButtonsFactory);

            var enemyUIFactory = new EnemyUIFactory(gameData, canvas, abilitiesManager);

            for (int i = 0; i < enemyCount; i++)
            {
                enemyViews[i] = enemyInitialisation.GetEnemies(i).GetComponent<EnemyView>();
                var enemyPanel = enemyUIFactory.GetEnemyStatsPanel(enemyViews[i], i);
                enemyViews[i].InitStatsPanel(enemyPanel, unitsUIPositionController);
            }

            var enemyController = new EnemyController(enemyModels, enemyViews, poolController, playersViews, abilitiesManager, 
                                            damageModifier, endScreenController, enemiesStatesController);

            var playerController = new PlayerController(playersModels, playersViews, inputController, poolController, damageModifier, 
                                            endScreenController, abilitiesManager, targetprovider, skillButtonsManager);

            var turnController = new TurnController(uIFields, enemyViews, playerController, enemyController);
            var skillButtonActiveStateController = new SkillButtonsActiveStateController(skillButtonsManager, playerController);
            var skillButtonCDStateController = new SkillButtonsCDStateController(skillButtonsManager, turnController, playerController);
            var targetController = new TargetController(enemyViews, enemyModels, turnController, gameData, inputController);

            endscreen.transform.SetSiblingIndex(5); //todo подумать, как сделать нормальнов

            var saveDataRepository = new SaveDataRepository(inputController,/* playersModels*/ enemyModels, skillButtonsManager, 
                                            startGameParametersManager);

            startGameParametersManager.ResetSavedData();

            controllersManager.Add(inputController);
            controllersManager.Add(poolController);
            controllersManager.Add(endScreenController);
            controllersManager.Add(playerController);
            controllersManager.Add(enemyController);
            controllersManager.Add(targetController);
            controllersManager.Add(turnController);
            controllersManager.Add(skillButtonActiveStateController);
            controllersManager.Add(skillButtonCDStateController);
            controllersManager.Add(saveDataRepository);
            controllersManager.Add(enemiesStatesController);
            controllersManager.Add(unitsUIPositionController);
        }
    }
}
