using PlayFab.ClientModels;
using PlayFab;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainPanelController : MonoBehaviour
{
    [SerializeField] private Button _loginButton;
    [SerializeField] private Button _createAccButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _showLBButton;
    [SerializeField] private OptionsPanelView _optionsPanelView;
    [SerializeField] private CreateAccountWindow _createAccountWindow;
    [SerializeField] private SignInWindow _signInWindow;
    [SerializeField] private GameObject _buttonHolder;
    [SerializeField] private GameObject _lbPanel;
    [SerializeField] private TMP_Text _leadersNameText;
    [SerializeField] private TMP_Text _leadersLevelsText;
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

        _loginButton.onClick.AddListener(ShowLoginWindow);
        _createAccButton.onClick.AddListener(ShowCreateAccountWindow);
        _startButton.onClick.AddListener(StartGame);
        _showLBButton.onClick.AddListener(ShowLeaderBoard);

        _optionsPanelView.Init();
        _soundManager.SubscribeMenuButtons();
        _soundManager.PlayMenuSound();

        _createAccountWindow.OnLogin += OnLoginSuccess;
        _signInWindow.OnLogin += OnLoginSuccess;
    }

    private void ShowLeaderBoard()
    {
        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
        {
            StatisticName = PlayFabConstants.LEVELS_COUNT,
            MaxResultsCount = 10

        }, OnLeaderBoardLoaded, OnError);
    }

    private void OnLeaderBoardLoaded(GetLeaderboardResult result)
    {
        _leadersNameText.text = "";
        _leadersLevelsText.text = "";

        var leaderboard = result.Leaderboard;
        var positionShift = 1;

        for (int i = 0; i < leaderboard.Count; i++)
        {          
            var leadNameString = $"{leaderboard[i].Position + positionShift}) {leaderboard[i].DisplayName}:\n";
            var leadlevelString = $"{leaderboard[i].StatValue}" + "\n";
            _leadersNameText.text += leadNameString;
            _leadersLevelsText.text += leadlevelString;
        }

        _lbPanel.SetActive(true);
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log(error.ErrorDetails);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void OnLoginSuccess()
    {
        _startButton.gameObject.SetActive(true);
        _showLBButton.gameObject.SetActive(true);
        _createAccButton.gameObject.SetActive(false);
        _loginButton.gameObject.SetActive(false);
    }

    private void ShowCreateAccountWindow()
    {
        _buttonHolder.SetActive(false);
        _createAccountWindow.gameObject.SetActive(true);
    }

    private void ShowLoginWindow()
    {
        _buttonHolder.SetActive(false);
        _signInWindow.gameObject.SetActive(true);
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
