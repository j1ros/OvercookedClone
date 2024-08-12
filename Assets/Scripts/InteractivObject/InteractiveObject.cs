using System.Collections.Generic;
using UnityEngine;

namespace Overcooked.InteractivObject
{
    public class InteractiveObject : MonoBehaviour
    {
        [SerializeField] private bool _canThrow;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private float _throwForce;
        [SerializeField] private Collider _collider;
        [SerializeField] private GameObject _interactiveObjectSelected;
        public bool CanThrow => _canThrow;

        private void Awake()
        {
            EventManager.StartListening(EventType.SelectInteractiveObject, SelectInteractiveObject);
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

        public void Throw(Vector3 moveDir)
        {
            _rb.AddForce(moveDir * _throwForce, ForceMode.Impulse);
            _collider.enabled = true;
            _rb.useGravity = true;
        }

        public void PickUp()
        {
            transform.localPosition = Vector3.zero;
            _collider.enabled = false;
            _rb.useGravity = false;
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}

