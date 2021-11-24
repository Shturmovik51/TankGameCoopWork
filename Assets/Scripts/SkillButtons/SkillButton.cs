using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame
{
    public class SkillButton
    {
        public Button Button { get; }
        public int AbilityID { get; }
        public Image SkillImage { get; }
        public TextMeshProUGUI CDText { get; }
        public int MaxCD { get; }
        public int CurrentCD { get; set; }
        public bool IsOnCD { get; set; }
        public bool IsActive { get; set; }

        public SkillButton(Button button, Ability ability)
        {
            Button = button;
            AbilityID = ability.ID;
            SkillImage = Button.GetComponent<Image>();
            SkillImage.sprite = ability.ElementIcon;     
            CDText = Button.GetComponentInChildren<TextMeshProUGUI>();
            CDText.gameObject.SetActive(false);
            MaxCD = ability.CD;
            CurrentCD = ability.CD;
        }
    }
}