using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace TankGame
{
    public class EnemyStatsPanel
    {
        private GameObject _statsPanel;
        private TextMeshProUGUI _titleText;
        private Image _elementIcon;
        private Image _healthBar;

        public EnemyStatsPanel(GameObject statsPanel)
        {
            _statsPanel = statsPanel;
            _titleText = _statsPanel.GetComponentInChildren<TextMeshProUGUI>();
            var images = _statsPanel.GetComponentsInChildren<Image>();
            _elementIcon = images[1];
            _healthBar = images[3];
        }

        public EnemyStatsPanel UpdateTitle(string text)
        {
            _titleText.text = text;
            return this;
        }

        public EnemyStatsPanel UpdateHP(float value)
        {
            _healthBar.fillAmount = value;
            return this;
        }

        public EnemyStatsPanel UpdateElement(Sprite image)
        {
            _elementIcon.sprite = image;
            return this;
        }
    }
}