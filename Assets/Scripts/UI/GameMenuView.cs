using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuView : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private OptionsPanelView _optionsPanelView;
    [SerializeField, HideInInspector] private SoundManager _soundManager;

    public void Init()
    {
        _soundManager = FindObjectOfType<SoundManager>();

        _continueButton.onClick.AddListener(OnClickContinueButton);
        _optionsButton.onClick.AddListener(() => _optionsPanelView.gameObject.SetActive(true));
        _exitButton.onClick.AddListener(() => SceneManager.LoadScene(0));
        _optionsPanelView.MusicSlider.onValueChanged.AddListener(_soundManager.SetMusicValue);
        _optionsPanelView.EffectsSlider.onValueChanged.AddListener(_soundManager.SetEffectsValue);

        _optionsPanelView.Init();
        
        _soundManager.PlayGameMainTheme();
        _soundManager.AddButton(_continueButton);
        _soundManager.AddButton(_optionsButton);
        _soundManager.AddButton(_exitButton);
        _soundManager.AddButton(_optionsPanelView.ConfirmButton);
        _soundManager.AddButton(_optionsPanelView.BackButton);
    }

    private void OnClickContinueButton()
    {
        gameObject.SetActive(false);
        _optionsPanelView.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ClearSubscribes()
    {
        _continueButton.onClick.RemoveAllListeners();
        _optionsButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();

        _optionsPanelView.MusicSlider.onValueChanged.RemoveAllListeners();
        _optionsPanelView.EffectsSlider.onValueChanged.RemoveAllListeners();
    }
}
