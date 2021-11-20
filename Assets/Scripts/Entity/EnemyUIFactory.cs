using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class EnemyUIFactory
    {
        private GameObject _statsPanelPref;
        private Transform _parentTransform;

        public EnemyUIFactory(GameData gameData, GameObject enemiesPanel)
        {
            _statsPanelPref = gameData.PrefabsData.EnemyUI;
            _parentTransform = enemiesPanel.transform;
        }

        public EnemyStatsPanel GetEntityStatsPanel()
        {
            var panelObject = Object.Instantiate(_statsPanelPref, _parentTransform);
            return new EnemyStatsPanel(panelObject);
        }
    }
}