using System.Collections.Generic;
using Overcooked.Counter;
using Overcooked.InteractivObject;
using UnityEngine;

namespace Overcooked
{
    public class PlayerInterapt : MonoBehaviour
    {
        [SerializeField] private Transform _placeForInteractiveObj;
        private InteractiveObject _selectedInteractiveObject;
        private BaseCounter _selectedCounter;
        private InteractiveObject _interactiveObject;
        public BaseCounter SelectedCounter => _selectedCounter;
        public InteractiveObject InteractiveObject => _interactiveObject;

        private void Awake()
        {
            EventManager.StartListening(EventType.Interapt, Interapt);
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventType.Interapt, Interapt);
        }

        public void SetSelectedCounter(BaseCounter counter)
        {
            if (counter != _selectedCounter)
            {
                _selectedCounter = counter;
                EventManager.TriggerEvent(EventType.SelectCounter, new Dictionary<EventMessageType, object> { { EventMessageType.Counter, _selectedCounter } });
            }
        }

        public void SetSelectedInteractiveObject(InteractiveObject interactiveObject)
        {
            if (interactiveObject != _selectedInteractiveObject)
            {
                _selectedInteractiveObject = interactiveObject;
                EventManager.TriggerEvent(EventType.SelectInteractiveObject, new Dictionary<EventMessageType, object> { { EventMessageType.InteractiveObject, _selectedInteractiveObject } });
            }
        }

        private void Interapt(Dictionary<EventMessageType, object> message)
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
    }
}
