using System.Collections.Generic;
using UnityEngine;
using Overcooked.InteractivObject;

namespace Overcooked.Counter
{
    public class BaseCounter : MonoBehaviour, IInterapt
    {
        [SerializeField] protected GameObject _counterSelected;
        [SerializeField] private Transform _placeForInteractiveObj;
        protected InteractiveObject _interactiveObject;

        private void Awake()
        {
            EventManager.StartListening(EventType.SelectCounter, SelectCounter);
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventType.SelectCounter, SelectCounter);
        }

        private void SelectCounter(Dictionary<string, object> message)
        {
            if ((message["counter"] as BaseCounter) == this)
            {
                _counterSelected.SetActive(true);
            }
            else
            {
                _counterSelected.SetActive(false);
            }
        }

        public virtual InteractiveObject Interapt(InteractiveObject interactiveObj)
        {
            if (_interactiveObject == null)
            {
                _interactiveObject = interactiveObj;
                PlaceInteractiveObj(interactiveObj);
                return null;
            }
            else
            {
                if (interactiveObj == null)
                {
                    InteractiveObject returnedInteractiveObj = _interactiveObject;
                    _interactiveObject = null;
                    return returnedInteractiveObj;
                }
                else
                {
                    //-- check can add interactiveObj to _interactiveObj if recipe confirm this
                }
            }

            return interactiveObj;
        }

        protected void PlaceInteractiveObj(InteractiveObject interactiveObj)
        {
            interactiveObj.gameObject.transform.SetParent(_placeForInteractiveObj, false);
        }
    }
}
