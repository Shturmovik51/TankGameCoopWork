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

        public EnemyStatsPanel GetEnemyStatsPanel(EnemyModel enemyModel, int iD)
        {
            var panelObject = Object.Instantiate(_statsPanelPref, _parentTransform);
            var panel = new EnemyStatsPanel(panelObject);

            panel.UpdateTitle($"Enemy {iD+1}")
                 .UpdateElement(enemyModel.Ability.ElementIcon)
                 .SetDeathIcon(enemyModel.Ability.DeathIcon);

            return panel;
        }
    }
}