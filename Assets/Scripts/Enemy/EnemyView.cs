using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TankGame
{
    public class EnemyView : MonoBehaviour, IDamagable, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Action<EnemyView> OnClickMe;
        public Action<EnemyView> OnPointerEnterMe;
        public Action OnPointerExitMe;
        public Action<int, IDamagable, AbilityType> OnTakeDamage { get; set; }
        public Action OnChangeTurn;
        public event Action OnReadyToShoot;

        [SerializeField] private Transform _shellStartPosition;
        [SerializeField] private Transform _tankTower;
        [SerializeField] private Rigidbody _tankRigidbody;
        [SerializeField] private ParticleSystem _explosionBody;
        [SerializeField] private ParticleSystem _explosionTover;
        [SerializeField] private BoxCollider _tankCollider;
        [SerializeField] private ParticleSystem _shootEffect;
        [SerializeField] private ParticleSystem _flyEffect;

        private Quaternion _startDirection;
        private Quaternion _targetDirection;

        private bool _isOnRotation;
        private float _lerpProgress;
        private float _rotationTime = 1;
        private EntiTyStatsPanel _tankStatsPanel;

        public ParticleSystem FlyEffect => _flyEffect;
        public bool IsOnRotation => _isOnRotation;
        public Rigidbody TankRigidbody => _tankRigidbody;
        public BoxCollider TankCollider => _tankCollider;

        public void Shoot(GameObject shell, int shootForce)
        {     
            shell.transform.position = _shellStartPosition.position;
            shell.transform.rotation = _shellStartPosition.transform.rotation;
            shell.SetActive(true);
            var shellRigidBody = shell.GetComponent<Rigidbody>();
            shellRigidBody.velocity = Vector3.zero;
            shellRigidBody.AddForce(_shellStartPosition.forward * shootForce, ForceMode.Impulse);
            _shootEffect.Play();
        }

        public void InitStatsPanel(EntiTyStatsPanel tankStatsPanel, UnitsUIPositionController unitsUIPositionController)
        {
            _tankStatsPanel = tankStatsPanel;
            unitsUIPositionController.AddUIElement(transform, _tankStatsPanel.StatsPanel);
        }

        public void SetStartRotationParameters(Transform target)
        {
            _startDirection = _tankTower.transform.rotation;
            _targetDirection = Quaternion.LookRotation(target.transform.position - _tankTower.transform.position);
            _isOnRotation = true;
        }

        public void Rotate(float deltaTime)
        {
            if (!_isOnRotation) return;

            _lerpProgress += deltaTime / _rotationTime;
            
            _tankTower.transform.rotation = Quaternion.Lerp(_startDirection, _targetDirection, _lerpProgress);

            if (_lerpProgress > 1)
            {
                _lerpProgress = 0;
                _isOnRotation = false;
                OnReadyToShoot?.Invoke();
            }
        }

        public void UpdateHPBar(float barValue)
        {
            _tankStatsPanel.UpdateHP(barValue);
        }

        public void Explosion()
        {
            var explosionPosition = transform.position;
            var explRadius = 3;

            Collider[] colliders = Physics.OverlapSphere(explosionPosition, explRadius);

            foreach (var hit in colliders)
            {
                Rigidbody hitRB = hit.GetComponent<Rigidbody>();
                if (hitRB != null && hitRB != _tankRigidbody)
                {
                    hitRB.isKinematic = false;
                    hitRB.AddExplosionForce(200, transform.position, explRadius, 3.0f, ForceMode.Impulse);
                }
            }
            _explosionBody.gameObject.SetActive(true);
            _explosionTover.gameObject.SetActive(true);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickMe?.Invoke(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEnterMe?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnPointerExitMe?.Invoke();
        }    
    }
}