using System.Collections.Generic;
using Overcooked.Counter;
using UnityEngine;

namespace Overcooked
{
    public class PlayerInterapt : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private GameInput _gameInput;
        [SerializeField] private float _interaptDistance;
        private Vector3 _lastInteraptVector;
        private BaseCounter _selectedCounter;

        private void Awake()
        {
            EventManager.StartListening(EventType.Interapt, Interapt);
        }

        private void Update()
        {
            HandleInterapt();
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventType.Interapt, Interapt);
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
                _selectedCounter.Interapt();
            }
        }
    }
}
