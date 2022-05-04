using UnityEngine;

namespace TankGame
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private int _effectsCount;
        [SerializeField] private Transform[] _playersStartPos;
        [SerializeField] private Transform[] _enemiesStartPos;
        [SerializeField] private UIFields _uIFields;
        [SerializeField] private Canvas _canvas;

        private ControllersManager _controllersManager;
        private GameData _gameData;


        private void Start()
        {
            _controllersManager = new ControllersManager();
            _gameData = (GameData) Resources.Load("GameData");

            new GameInitializator(_controllersManager, _gameData, _effectsCount, this, _playersStartPos, 
                                    _enemiesStartPos, _uIFields, _canvas);

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
            _controllersManager.LocalFixedUpdate(fixedDeltaTime);
        }

        private void OnDestroy()
        {
            _controllersManager.CleanUp();
        }
    }
}

