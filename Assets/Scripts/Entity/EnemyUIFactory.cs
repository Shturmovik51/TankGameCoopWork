using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class EnemyUIFactory
    {
        private GameObject _statsPanelPref;
        private Transform _parentTransform;
        private AbilitiesManager _abilitiesManager;

        public EnemyUIFactory(GameData gameData, GameObject enemiesPanel, AbilitiesManager abilitiesManager)
        {
            _statsPanelPref = gameData.PrefabsData.EnemyUI;
            _parentTransform = enemiesPanel.transform;
            _abilitiesManager = abilitiesManager;
        }

        public EnemyStatsPanel GetEnemyStatsPanel(EnemyModel enemyModel, int iD)
        {
            var panelObject = Object.Instantiate(_statsPanelPref, _parentTransform);
            var panel = new EnemyStatsPanel(panelObject);

            panel.UpdateTitle($"Enemy {iD+1}")
                 .UpdateElement(_abilitiesManager.GetAbility(enemyModel.AbilityID).ElementIcon)
                 .SetDeathIcon(_abilitiesManager.GetAbility(enemyModel.AbilityID).DeathIcon);

            return panel;
        }
    }
}