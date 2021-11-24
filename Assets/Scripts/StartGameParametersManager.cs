using UnityEngine;

namespace TankGame
{
    public class StartGameParametersManager : MonoBehaviour
    {
        [SerializeField] private int _lifesCount;
        [SerializeField] private float _difficultIndex;
        private SavedData _savedData; 

        public int LifesCount => _lifesCount;
        public float DifficultIndex => _difficultIndex;
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
    }
}