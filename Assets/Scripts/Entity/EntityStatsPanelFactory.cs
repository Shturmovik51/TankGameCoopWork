using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class EntityStatsPanelFactory
    {
        private GameObject _statsPanelPref;
        private Transform _parentTransform;

        public EntityStatsPanelFactory(GameObject statsPanelPref, Transform parentTransform)
        {
            _statsPanelPref = statsPanelPref;
            _parentTransform = parentTransform;
        }

        public EntityStatsPanel GetEntityStatsPanel()
        {
            var panelObject = Object.Instantiate(_statsPanelPref, _parentTransform);
            return new EntityStatsPanel(panelObject);
        }
    }
}