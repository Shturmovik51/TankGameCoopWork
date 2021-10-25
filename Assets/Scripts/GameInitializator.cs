namespace TankGame
{
    public sealed class GameInitializator
    {
        public GameInitializator(ControllersManager controllersManager, GameData gameData)
        {
            var inputController = new InputController(gameData);

            controllersManager.Add(inputController);
        }
    }
}
