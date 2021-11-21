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
        private Sprite _deathIcon;

        public EnemyStatsPanel(GameObject statsPanel)
        {
            _statsPanel = statsPanel;
            _titleText = _statsPanel.GetComponentInChildren<TextMeshProUGUI>();
            var images = _statsPanel.GetComponentsInChildren<Image>();

            for (int i = 0; i < images.Length; i++)
            {
                if(images[i].type == Image.Type.Filled)
                    _healthBar = images[i];

                if(images[i].sprite == null)
                    _elementIcon = images[i];
            }
        }

        public EnemyStatsPanel UpdateTitle(string text)
        {
            _titleText.text = text;
            return this;
        }

        public EnemyStatsPanel UpdateHP(float value)
        {
            _healthBar.fillAmount = value;

            if (value == 0)
                _elementIcon.sprite = _deathIcon;

            return this;
        }

        public EnemyStatsPanel UpdateElement(Sprite image)
        {
            _elementIcon.sprite = image;
            return this;
        }

        public void SetDeathIcon(Sprite image)
        {
            _deathIcon = image;
        }
    }
}