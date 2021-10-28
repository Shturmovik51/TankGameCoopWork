using TMPro;
using UnityEngine;

namespace TankGame
{
    [System.Serializable]
    public struct UIFields
    {
        [SerializeField] private TextMeshProUGUI _turnText;

        public TextMeshProUGUI TurnText => _turnText;
    }
}