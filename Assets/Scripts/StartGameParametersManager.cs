using UnityEngine;

namespace TankGame
{
    public class StartGameParametersManager : MonoBehaviour
    {
        [SerializeField] private int _lifesCount;
        [SerializeField] private float _difficultIndex;
        private SavedData _savedData;
        private int _levelsCount;
        private float _playerLevelPoints;

        public int LifesCount => _lifesCount;
        public float DifficultIndex => _difficultIndex;
        public int LevelsCount => _levelsCount;
        public float PlayerLevelPoints => _playerLevelPoints;
        public SavedData SavedData => _savedData;

        private void Awake()
        {
            StartGameParametersManager[] objs = FindObjectsOfType<StartGameParametersManager>();

            if (objs.Length > 1)
            {
                Destroy(this.gameObject);
            }

            DontDestroyOnLoad(this.gameObject);
        }

        public void IncreaseDifficultIndex()
        {
            _difficultIndex += 0.1f;
        }

        public void SetStartGameParameters()
        {
            _difficultIndex = 0;
            _levelsCount= 0;
            _playerLevelPoints = 0;
            _lifesCount = 3;
        }

        public void DecreaseLifeCount()
        {
            _lifesCount--;
        }

        public void InitsavedData(SavedData savedData)
        {
            _savedData = savedData;
        }
        public void ResetSavedData()
        {
            _savedData = null;
        }

        public void IncreaseLevelCount()
        {
            _levelsCount++;
        }

        public void CalculateLevelPoints()
        {
            _playerLevelPoints = 10 + (_levelsCount * _difficultIndex);
        } 
    }
}

public class PlayFabConstants
{
    public const string LEVEL_POINTS = "LP";
    public const string LEVELS_COUNT = "Max Levels Count";
}