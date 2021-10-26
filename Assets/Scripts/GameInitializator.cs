using UnityEngine;

namespace TankGame
{
    public sealed class GameInitializator
    {
        public GameInitializator(ControllersManager controllersManager, GameData gameData, int effectsCount,
                    GameManager gameManager, Transform playerPosition)
        {
            var inputController = new InputController(gameData);
            var poolController = new PoolController(gameData, effectsCount, gameManager);
            var playerModel = new PlayerModel(gameData.PlayerModelData.TankPrefab);
            var playerFactory = new PlayerFactory(playerModel);
            var playerInitialization = new PlayerInitialization(playerFactory, playerPosition);

            var playerView = new PlayerView();
            var playerController = new PlayerController(playerModel, playerView, inputController, playerInitialization.GetPlayer(),
                                        poolController, gameManager);



            controllersManager.Add(inputController);
            controllersManager.Add(poolController); 
            controllersManager.Add(playerController);
        }
    }
}
