using UnityEngine;

namespace TankGame
{
    public class RoundController : MonoBehaviour
    {
        [SerializeField] private int _lifesCount;
        [SerializeField] private float _difficultIndex;

        public int LifesCount => _lifesCount;
        public float DifficultIndex => _difficultIndex;

        private void Awake()
        {
            RoundController[] objs = FindObjectsOfType<RoundController>();

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
    }
}