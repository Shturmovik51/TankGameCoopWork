using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame
{
    public class SkillButton
    {
        public Button Button { get; }
        public AbilityType AbilityType { get; }
        public Image SkillImage { get; }
        public TextMeshProUGUI CDText { get; }
        public int MaxCD { get; }
        public int CurrentCD { get; set; }
        public bool IsActive { get; set; }

        public SkillButton(Button button, int maxCD, AbilityType abilityType, Sprite skillIcon)
        {
            Button = button;
            AbilityType = abilityType;
            SkillImage = Button.GetComponent<Image>();
            SkillImage.sprite = skillIcon;     
            CDText = Button.GetComponentInChildren<TextMeshProUGUI>();
            CDText.gameObject.SetActive(false);
            MaxCD = maxCD;
            CurrentCD = maxCD;
        }
    }
}