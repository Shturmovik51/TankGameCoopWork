using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class UnitsUIPositionController : IUpdatable, IController
    {
        private Dictionary<Transform, GameObject> _elementsForPositioning;
        private Camera _camera;
        public UnitsUIPositionController()
        {
            _elementsForPositioning = new Dictionary<Transform, GameObject>();
            _camera = Camera.main;
        }

        public void LocalUpdate(float deltaTime)
        {
            foreach (var element in _elementsForPositioning)
            {
                element.Value.transform.position = _camera.WorldToScreenPoint(element.Key.position + Vector3.up*2);                
            }
        }


        public void AddUIElement(Transform owner, GameObject element)
        {
            _elementsForPositioning.Add(owner, element);
        }        
    }
}