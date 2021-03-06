using UnityEngine;

namespace TankGame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int _effectsCount;
        [SerializeField] private Transform _playerStartPos;
        [SerializeField] private Transform _enemyStartPos;
        [SerializeField] private UIFields _uIFields;

        private ControllersManager _controllersManager;
        private GameData _gameData;


        private void Start()
        {
            _controllersManager = new ControllersManager();
            _gameData = (GameData) Resources.Load("GameData");

            new GameInitializator(_controllersManager, _gameData, _effectsCount, this, _playerStartPos, _enemyStartPos, _uIFields);

            _controllersManager.Initialization();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _controllersManager.LocalUpdate(deltaTime);
        }

        private void LateUpdate()
        {
            var deltaTime = Time.deltaTime;
            _controllersManager.LocalLateUpdate(deltaTime);
        }

        private void FixedUpdate()
        {
            var fixedDeltaTime = Time.fixedDeltaTime;
            _controllersManager.LocalLateUpdate(fixedDeltaTime);
        }

        private void OnDestroy()
        {
            _controllersManager.CleanUp();
        }
    }
}

