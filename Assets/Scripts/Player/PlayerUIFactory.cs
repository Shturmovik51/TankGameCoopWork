using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class PlayerUIFactory
    {
        private GameObject _statsPanelPref;
        private Transform _parentTransform;
        private AbilitiesManager _abilitiesManager;

        public PlayerUIFactory(GameData gameData, Canvas canvas, AbilitiesManager abilitiesManager)
        {
            _statsPanelPref = gameData.PrefabsData.PlayerUI;
            _parentTransform = canvas.transform;
            _abilitiesManager = abilitiesManager;
        }

        public EntiTyStatsPanel GetPlayerStatsPanel(PlayerView playerView, int iD)
        {
            var panelObject = Object.Instantiate(_statsPanelPref, _parentTransform);
            var panel = new EntiTyStatsPanel(panelObject);

            panel.UpdateTitle($"Player {iD + 1}");
                 //.UpdateElement(_abilitiesManager.GetAbility(playerView.gameObject).ElementIcon)
                 //.SetDeathIcon(_abilitiesManager.GetAbility(playerView.gameObject).DeathIcon);

            return panel;
        }
    }
}
