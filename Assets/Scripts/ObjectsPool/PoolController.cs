using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class PoolController : IInitializable, IController
    {
        private int _effectsCount;
        private GameStarter _gameManager;

        private GameObject _hitEffectSample;
        private GameObject _tankExplosionSample;
        private GameObject _shellSample;

        private Transform _hitEffectContainer;
        private Transform _tankExplosionsContainer;
        private Transform _shellContainer;

        private List<GameObject> _hitEffects;
        private List<GameObject> _tankExplosions;
        private List<GameObject> _shells;
        public PoolController(GameData gameData, int effectsCount, GameStarter gamemanager)
        {
            _hitEffectSample = gameData.PrefabsData.HitEffect;
            _tankExplosionSample = gameData.PrefabsData.TankExplosion;
            _shellSample = gameData.PrefabsData.Shell;

            _effectsCount = effectsCount;
            _gameManager = gamemanager;
        }

        public void Initialization()
        {
            _hitEffectContainer = new GameObject(name: "ShellExplosionsContainer").transform;
            _tankExplosionsContainer = new GameObject(name: "TankExplosionsContainer").transform;
            _shellContainer = new GameObject(name: "ShellsContainer").transform;

            _hitEffectContainer.parent = _gameManager.transform;
            _tankExplosionsContainer.parent = _gameManager.transform;
            _shellContainer.parent = _gameManager.transform;

            _hitEffects = new List<GameObject>(_effectsCount);
            _tankExplosions = new List<GameObject>(_effectsCount);
            _shells = new List<GameObject>(_effectsCount);

            for (int i = 0; i < _effectsCount; i++)
            {
                InitCollection(_hitEffectSample, _hitEffects, _hitEffectContainer);
                InitCollection(_tankExplosionSample, _tankExplosions, _tankExplosionsContainer);
                InitCollection(_shellSample, _shells, _shellContainer);
            }

            for (int i = 0; i < _shells.Count; i++)
            {
                _shells[i].GetComponent<Shell>().OnHit += GetHitEffect;
            }
        }

        private void InitCollection(GameObject obj, List<GameObject> collection, Transform container)
        {
            var poolObject = Object.Instantiate(obj, container);
            poolObject.SetActive(false);
            collection.Add(poolObject);
        }

        public void GetHitEffect(Transform transform)
        {
            var hitEffect = GetObject(_hitEffects);
            hitEffect.transform.position = transform.position;
            hitEffect.SetActive(true);
        }

        public GameObject GetTankExplosion()
        {
            return GetObject(_tankExplosions);
        }

        public GameObject GetShell()
        {
            var shell = GetObject(_shells);
            shell.GetComponent<Shell>().SetIdleState();
            return shell;
        }

        private GameObject GetObject(List<GameObject> effectPool)
        {
            var effect = effectPool[0];
            effectPool.Remove(effect);
            _gameManager.StartCoroutine(ObjectLiveTimer(effect, effectPool));
            return effect;      
        }

        private IEnumerator ObjectLiveTimer(GameObject effect, List<GameObject> effectPool)
        {
            yield return new WaitForSeconds(10);
            ReturnObjectToPoll(effect, effectPool);
        }

        private void ReturnObjectToPoll(GameObject effect, List<GameObject> effectPool)
        {
            effect.SetActive(false);
            effectPool.Add(effect);
        }
    }
}