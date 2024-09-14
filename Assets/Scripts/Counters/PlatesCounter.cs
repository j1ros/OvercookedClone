using System.Collections;
using System.Collections.Generic;
using Overcooked.Data;
using Overcooked.General;
using Overcooked.InteractivObject;
using UnityEngine;

namespace Overcooked.Counter
{
    public class PlatesCounter : BaseCounter
    {
        [SerializeField] private float _timeToRespPlate;
        [SerializeField] private InteractiveSO _plateSO;
        [SerializeField] private float _indent;
        private List<InteractiveObject> _interactiveObjects;

        protected new void Awake()
        {
            EventManager.StartListening(EventType.Delivery, Delivery);
            base.Awake();
        }

        protected new void OnDestroy()
        {
            EventManager.StopListening(EventType.Delivery, Delivery);
            base.OnDestroy();
        }

        private void Delivery(Dictionary<EventMessageType, object> data)
        {
            StartCoroutine(SpawnPlate());
        }

        IEnumerator SpawnPlate()
        {
            yield return new WaitForSeconds(_timeToRespPlate);
            InteractiveObject newPlate = ObjectManager.Instance.InstantiateInteractiveObject(_plateSO);
            PlaceInteractiveObj(newPlate);
        }

        protected override void PlaceInteractiveObj(InteractiveObject interactiveObj)
        {
            if (interactiveObj == null)
                return;
            if (_interactiveObjects == null)
                _interactiveObjects = new List<InteractiveObject>();

            interactiveObj.gameObject.transform.SetParent(_placeForInteractiveObj, false);
            interactiveObj.gameObject.transform.localPosition += new Vector3(0, _interactiveObjects.Count * _indent, 0);
            _interactiveObjects.Add(interactiveObj);
        }

        public override InteractiveObject Interapt(InteractiveObject interactiveObj)
        {
            if (interactiveObj != null)
                return interactiveObj;

            if (_interactiveObjects.Count > 0)
            {
                InteractiveObject returnedObj = _interactiveObjects[_interactiveObjects.Count - 1];
                _interactiveObjects.RemoveAt(_interactiveObjects.Count - 1);
                return returnedObj;
            }
            else
            {
                return null;
            }
        }
    }
}
