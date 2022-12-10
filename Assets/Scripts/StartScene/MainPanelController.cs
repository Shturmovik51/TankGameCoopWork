using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPanelController : MonoBehaviour
{
    [SerializeField] private Button _loginButton;
    [SerializeField] private Button _createAccButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private OptionsPanelView _optionsPanelView;
    [SerializeField, HideInInspector] private SoundManager _soundManager;

    private void Start()
    {
        Time.timeScale = 1;
        _soundManager = FindObjectOfType<SoundManager>();
        _soundManager.ResetSoundManager();

        _optionsButton.onClick.AddListener(() => _optionsPanelView.gameObject.SetActive(true));
        _exitButton.onClick.AddListener(Application.Quit);
        _optionsPanelView.MusicSlider.onValueChanged.AddListener(_soundManager.SetMusicValue);
        _optionsPanelView.EffectsSlider.onValueChanged.AddListener(_soundManager.SetEffectsValue);

        _loginButton.onClick.AddListener(() => SceneManager.LoadScene(1));

        _optionsPanelView.Init();
        _soundManager.SubscribeMenuButtons();
        _soundManager.PlayMenuSound();
    }

    private void OnDisable()
    {
        _loginButton.onClick.RemoveAllListeners();
        _createAccButton.onClick.RemoveAllListeners();
        _optionsButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();

        _optionsPanelView.MusicSlider.onValueChanged.RemoveAllListeners();
        _optionsPanelView.EffectsSlider.onValueChanged.RemoveAllListeners();
    }
}
