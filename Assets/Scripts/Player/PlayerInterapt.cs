using System.Collections.Generic;
using Overcooked.Counter;
using Overcooked.InteractivObject;
using UnityEngine;

namespace Overcooked
{
    public class PlayerInterapt : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMaskCounter;
        [SerializeField] private LayerMask _layerMaskInteractiveObject;
        [SerializeField] private GameInput _gameInput;
        [SerializeField] private float _interaptDistance;
        [SerializeField] private Transform _placeForInteractiveObj;
        [SerializeField] private Transform _playerInteraptTransform;
        [SerializeField] private Vector3 _halfExtents;
        private InteractiveObject _selectedInteractiveObject;
        private Vector3 _lastInteraptVector;
        private BaseCounter _selectedCounter;
        private InteractiveObject _interactiveObject;
        public Vector3 LastInteraptVector => _lastInteraptVector;
        public BaseCounter SelectedCounter => _selectedCounter;
        public InteractiveObject InteractiveObject => _interactiveObject;

        private void Awake()
        {
            EventManager.StartListening(EventType.Interapt, Interapt);
            // EventManager.StartListening(EventType.PickUpInteractiveObject, PickUpInteractiveObject);
        }

        private void Update()
        {
            HandleInterapt();
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventType.Interapt, Interapt);
            // EventManager.StopListening(EventType.PickUpInteractiveObject, PickUpInteractiveObject);
        }

        private void HandleInterapt()
        {
            Vector2 inputVector = _gameInput.GetMovementVectorNormilized();
            Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
            bool counterIsFind = false;

            if (moveDir != Vector3.zero)
                _lastInteraptVector = moveDir;

            if (Physics.BoxCast(_playerInteraptTransform.position, _halfExtents, _lastInteraptVector, out RaycastHit raycastHitCounter, Quaternion.identity, _interaptDistance, _layerMaskCounter))
                if (raycastHitCounter.transform.TryGetComponent(out BaseCounter baseCounter))
                {
                    counterIsFind = true;
                    if (baseCounter != _selectedCounter)
                        SetSelectedCounter(baseCounter);
                }
                else
                {
                    SetSelectedCounter(null);
                }
            else
                SetSelectedCounter(null);
            if (Physics.BoxCast(_playerInteraptTransform.position, _halfExtents, _lastInteraptVector, out RaycastHit raycastHitInteractiveObj, Quaternion.identity, _interaptDistance, _layerMaskInteractiveObject)
                && !counterIsFind)
                if (raycastHitInteractiveObj.transform.TryGetComponent(out InteractiveObject interactiveObject))
                {
                    if (interactiveObject != _selectedInteractiveObject)
                        SetSelectedInteractiveObject(interactiveObject);
                }
                else
                {
                    SetSelectedInteractiveObject(null);
                }
            else
                SetSelectedInteractiveObject(null);
        }

        private void SetSelectedCounter(BaseCounter counter)
        {
            _selectedCounter = counter;
            EventManager.TriggerEvent(EventType.SelectCounter, new Dictionary<string, object> { { "counter", _selectedCounter } });
        }

        private void SetSelectedInteractiveObject(InteractiveObject interactiveObject)
        {
            if (interactiveObject != _selectedInteractiveObject)
            {
                _selectedInteractiveObject = interactiveObject;
                EventManager.TriggerEvent(EventType.SelectInteractiveObject, new Dictionary<string, object> { { "interactiveObject", _selectedInteractiveObject } });
            }
        }

        private void Interapt(Dictionary<string, object> message)
        {
            if (_selectedInteractiveObject != null)
            {
                SetInteractiveObject(_selectedInteractiveObject);
                TakeInHand(_interactiveObject);
                return;
            }

            if (_selectedCounter != null)
            {
                SetInteractiveObject(_selectedCounter.Interapt(_interactiveObject));
                if (_interactiveObject != null)
                {
                    TakeInHand(_interactiveObject);
                }
                return;
            }
            if (_interactiveObject != null)
            {
                //drop on ground
                return;
            }
        }

        public void SetInteractiveObject(InteractiveObject interactiveObject)
        {
            _interactiveObject = interactiveObject;
        }

        private void TakeInHand(InteractiveObject interactiveObject)
        {
            interactiveObject.PickUp();
            interactiveObject.gameObject.transform.SetParent(_placeForInteractiveObj, false);
        }

        // private void PickUpInteractiveObject(Dictionary<string, object> message)
        // {

        // }
    }
}
