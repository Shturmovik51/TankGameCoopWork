using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

namespace TankGame
{
    public class EndScreenController : MonoBehaviour, IInitializable, ICleanable, IController
    {
        public event Action OnTakeLife;

        [SerializeField] private GameObject _endScreenPanel;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _tryAgainButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private TextMeshProUGUI _winText;
        [SerializeField] private TextMeshProUGUI _loseText;
        [SerializeField] private TextMeshProUGUI _lifesCountText;
        private RoundController _roundController;

        public void Initialization()
        {
            _restartButton.onClick.AddListener(OnClickRestartButton);
            _tryAgainButton.onClick.AddListener(OnClickTryAgain);
            _continueButton.onClick.AddListener(OnClickContinueButton);
        }

        public void CleanUp()
        {
            _restartButton.onClick.RemoveAllListeners();
            _tryAgainButton.onClick.RemoveAllListeners();
            _continueButton.onClick.RemoveAllListeners();
        }

        public void SetRoundController(RoundController roundController)
        {
            _roundController = roundController;
        }

        public void StartLoseScreen(int lifesCount)
        {
            _endScreenPanel.SetActive(true);
            Time.timeScale = 0;
           
            _lifesCountText.text = lifesCount.ToString();

            if (lifesCount == 0)
                _tryAgainButton.interactable = false;

            _loseText.gameObject.SetActive(true);
            _tryAgainButton.gameObject.SetActive(true);

        }

        public void StartWinScreen()
        {
            _endScreenPanel.SetActive(true);
            Time.timeScale = 0;

            _winText.gameObject.SetActive(true);
            _continueButton.gameObject.SetActive(true);
        }

        private void OnClickRestartButton()
        {
            _roundController.SetStartGameParameters();
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
        private void OnClickTryAgain()
        {
            _roundController.DecreaseLifeCount();
            OnTakeLife?.Invoke();
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
        private void OnClickContinueButton()
        {
            _roundController.IncreaseDifficultIndex();
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }

    }
}
