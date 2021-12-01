using UnityEngine;

namespace TankGame
{
    public class PlayerFactory
    {
        private PlayerModel[] _playerModels;

        public PlayerFactory(GameData gameData, StartGameParametersManager startGameParametersManager)
        {
            var models = gameData.PlayerBase.PlayerSamples;
            _playerModels = new PlayerModel[models.Count];

            for (int i = 0; i < models.Count; i++)
            {
                _playerModels[i] =  new PlayerModel(models[i], startGameParametersManager);
            }
        }

        public Transform[] CreatePlayers()
        {
            var playerObjects = new Transform[_playerModels.Length];

            for (int i = 0; i < _playerModels.Length; i++)
            {
                playerObjects[i] = Object.Instantiate(_playerModels[i].Tank).transform;
            }

            return playerObjects;
        }        

        public PlayerModel[] GetPlayersModels()
        {
            return _playerModels;
        }

    }
}