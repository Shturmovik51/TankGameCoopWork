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
            var playerModel = new PlayerModel(gameData.PlayerModelData);
            var playerFactory = new PlayerFactory(playerModel);
            var playerInitialization = new PlayerInitialization(playerFactory, playerPosition);
            var playerView = playerInitialization.GetPlayer().GetComponent<PlayerView>();
            var playerController = new PlayerController(playerModel, playerView, inputController, poolController);



            controllersManager.Add(inputController);
            controllersManager.Add(poolController); 
            controllersManager.Add(playerController);
        }
    }
}
