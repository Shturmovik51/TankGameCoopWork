using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class PoolController : IInitializable, IController
    {
        private int _effectsCount;
        private GameManager _gameManager;

        private GameObject _shellExplosionSample;
        private GameObject _tankExplosionSample;
        private GameObject _shellSample;

        private Transform _shellExplosionsContainer;
        private Transform _tankExplosionsContainer;
        private Transform _shellContainer;

        private List<GameObject> _shellExplosions;
        private List<GameObject> _tankExplosions;
        private List<GameObject> _shells;
        public PoolController(GameData gameData, int effectsCount, GameManager gamemanager)
        {
            _shellExplosionSample = gameData.PrefabsData.ShellExplosion;
            _tankExplosionSample = gameData.PrefabsData.TankExplosion;
            _shellSample = gameData.PrefabsData.Shell;

            _effectsCount = effectsCount;
            _gameManager = gamemanager;
        }

        public void Initialization()
        {
            _shellExplosionsContainer = new GameObject(name: "ShellExplosionsContainer").transform;
            _tankExplosionsContainer = new GameObject(name: "TankExplosionsContainer").transform;
            _shellContainer = new GameObject(name: "ShellsContainer").transform;

            _shellExplosionsContainer.parent = _gameManager.transform;
            _tankExplosionsContainer.parent = _gameManager.transform;
            _shellContainer.parent = _gameManager.transform;

            _shellExplosions = new List<GameObject>(_effectsCount);
            _tankExplosions = new List<GameObject>(_effectsCount);
            _shells = new List<GameObject>(_effectsCount);

            for (int i = 0; i < _effectsCount; i++)
            {
                InitCollection(_shellExplosionSample, _shellExplosions, _shellExplosionsContainer);
                InitCollection(_tankExplosionSample, _tankExplosions, _tankExplosionsContainer);
                InitCollection(_shellSample, _shells, _shellContainer);
            }
        }

        private void InitCollection(GameObject obj, List<GameObject> collection, Transform container)
        {
            var poolObject = Object.Instantiate(obj, container);
            poolObject.SetActive(false);
            collection.Add(poolObject);
        }

        public GameObject GetShellExplosion()
        {
            return GetObject(_shellExplosions);
        }

        public GameObject GetTankExplosion()
        {
            return GetObject(_tankExplosions);
        }

        public GameObject GetShell()
        {
            return GetObject(_shells);
        }

        private GameObject GetObject(List<GameObject> effectPool)
        {
            var effect = effectPool[0];
            effectPool.Remove(effect);
            _gameManager.StartCoroutine(EffectTimer(effect, effectPool));
            return effect;
        }

        private IEnumerator EffectTimer(GameObject effect, List<GameObject> effectPool)
        {
            yield return new WaitForSeconds(10);
            ReturnHitEffectToPool(effect, effectPool);
        }

        private void ReturnHitEffectToPool(GameObject effect, List<GameObject> effectPool)
        {
            effect.SetActive(false);
            effectPool.Add(effect);
        }
    }
}