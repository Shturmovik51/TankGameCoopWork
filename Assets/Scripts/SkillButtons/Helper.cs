using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Helper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _helpPanel;
    [SerializeField] private TextMeshProUGUI _helpText;

    public TextMeshProUGUI HelpText => _helpText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _helpPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _helpPanel.SetActive(false);
    }
}
