using System.Collections.Generic;
using System.Collections;
using Overcooked.Data;
using UnityEngine;
using Overcooked.Counter;

namespace Overcooked.InteractivObject
{
    public class InteractiveObject : MonoBehaviour
    {
        [SerializeField] private bool _canThrow;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private float _throwForce;
        [SerializeField] private Collider _collider;
        [SerializeField] private GameObject _interactiveObjectSelected;
        [SerializeField] private float _drag = 0.6f;
        [SerializeField] private float _timeToFall = 0.6f;
        [SerializeField] private Vector3 _halfExtents;
        [SerializeField] private float _interaptDistance;
        [SerializeField] private LayerMask _layerMaskCounter;
        private Vector3 _throwDir;
        private InteractiveSO _interactiveSO;
        private bool _isThrowing = false;
        public InteractiveSO InteractiveSO => _interactiveSO;
        public bool CanThrow => _canThrow;

        private void Awake()
        {
            EventManager.StartListening(EventType.SelectInteractiveObject, SelectInteractiveObject);
        }

        private void Update()
        {
            if (_isThrowing)
            {
                HandleRaycast();
            }
        }

        private void HandleRaycast()
        {
            if (Physics.BoxCast(transform.position, _halfExtents, _throwDir, out RaycastHit raycastHitCounter, Quaternion.identity, _interaptDistance, _layerMaskCounter))
            {
                if (raycastHitCounter.transform.TryGetComponent(out BaseCounter baseCounter))
                {
                    InteractiveObject returnedObj = baseCounter.Interapt(this);
                    if (returnedObj == null)
                    {
                        PickUp();
                    }
                }
            }
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventType.SelectInteractiveObject, SelectInteractiveObject);
        }

        private void SelectInteractiveObject(Dictionary<EventMessageType, object> message)
        {
            if (message[EventMessageType.InteractiveObject] as InteractiveObject == this)
            {
                _interactiveObjectSelected.SetActive(true);
            }
            else
            {
                _interactiveObjectSelected.SetActive(false);
            }
        }

        public void SetSO(InteractiveSO interactiveSO)
        {
            _interactiveSO = interactiveSO;
        }

        public void Throw(Vector3 moveDir)
        {
            _throwDir = moveDir;
            _isThrowing = true;
            _rb.AddForce(moveDir * _throwForce, ForceMode.Impulse);
            _collider.enabled = true;
            if (moveDir == Vector3.zero)
            {
                _rb.useGravity = true;
                _rb.drag = _drag;
            }
            else
            {
                StartCoroutine(StartThrow());
            }
        }

        IEnumerator StartThrow()
        {
            yield return new WaitForSeconds(_timeToFall);
            _rb.useGravity = true;
            _rb.drag = _drag;
        }

        public void PickUp()
        {
            StopAllCoroutines();
            _isThrowing = false;
            transform.localPosition = Vector3.zero;
            _collider.enabled = false;
            _rb.useGravity = false;
            _rb.drag = 0f;
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}

