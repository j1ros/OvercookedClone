using System.Collections.Generic;
using Overcooked.Counter;
using Overcooked.InteractivObject;
using UnityEngine;

namespace Overcooked
{
    public class PlayerInterapt : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private GameInput _gameInput;
        [SerializeField] private float _interaptDistance;
        [SerializeField] private Transform _placeForInteractiveObj;
        private Vector3 _lastInteraptVector;
        private BaseCounter _selectedCounter;
        private InteractiveObject _interactiveObject;

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

            if (moveDir != Vector3.zero)
                _lastInteraptVector = moveDir;

            if (Physics.Raycast(transform.position, _lastInteraptVector, out RaycastHit raycastHit, _interaptDistance, _layerMask))
                if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
                {
                    if (baseCounter != _selectedCounter)
                    {
                        SetSelectedCounter(baseCounter);
                    }
                }
                else
                {
                    SetSelectedCounter(null);
                }
            else
            {
                SetSelectedCounter(null);
            }
        }

        private void SetSelectedCounter(BaseCounter counter)
        {
            if (counter != _selectedCounter)
            {
                _selectedCounter = counter;
                EventManager.TriggerEvent(EventType.SelectCounter, new Dictionary<string, object> { { "counter", _selectedCounter } });
            }
        }

        private void Interapt(Dictionary<string, object> message)
        {
            if (_selectedCounter == null)
            {
                //drop item
            }
            else
            {
                _interactiveObject = _selectedCounter.Interapt(_interactiveObject);
                if (_interactiveObject != null)
                {
                    TakeInHand(_interactiveObject);
                }
            }
        }

        private void TakeInHand(InteractiveObject interactiveObject)
        {
            interactiveObject.gameObject.transform.SetParent(_placeForInteractiveObj, false);
        }

        // private void PickUpInteractiveObject(Dictionary<string, object> message)
        // {

        // }
    }
}
