using UnityEngine;

namespace TankGame
{
    public class PlayerInitialization
    {
        private Transform[] _players;
        public PlayerInitialization(PlayerFactory playerFactory, Transform[] playersPositions)
        {
            _players = playerFactory.CreatePlayers();

            for (int i = 0; i < _players.Length; i++)
            {
                _players[i].position = playersPositions[i].position;
                _players[i].rotation = playersPositions[i].rotation;
            }

        }

        public PlayerView[] GetPlayersViews()
        {
            var playerViews = new PlayerView[_players.Length];

            for (int i = 0; i < _players.Length; i++)
            {
                playerViews[i] = _players[i].GetComponent<PlayerView>();
            }

            return playerViews;
        }
    }
}