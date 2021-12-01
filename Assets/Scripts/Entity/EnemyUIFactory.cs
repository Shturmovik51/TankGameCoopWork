using UnityEngine;

namespace TankGame
{
    public class EnemyUIFactory
    {
        private GameObject _statsPanelPref;
        private Transform _parentTransform;
        private AbilitiesManager _abilitiesManager;

        public EnemyUIFactory(GameData gameData, /*GameObject enemiesPanel*/Canvas canvas, AbilitiesManager abilitiesManager)
        {
            _statsPanelPref = gameData.PrefabsData.EnemyUI;
            _parentTransform = canvas.transform;
            _abilitiesManager = abilitiesManager;
        }

        public EntiTyStatsPanel GetEnemyStatsPanel(EnemyView enemyView, int iD)
        {
            var panelObject = Object.Instantiate(_statsPanelPref, _parentTransform);
            var panel = new EntiTyStatsPanel(panelObject);

            panel.UpdateTitle($"Enemy {iD+1}")
                 .UpdateElement(_abilitiesManager.GetAbility(enemyView.gameObject).ElementIcon)
                 .SetDeathIcon(_abilitiesManager.GetAbility(enemyView.gameObject).DeathIcon);

            return panel;
        }
    }
}