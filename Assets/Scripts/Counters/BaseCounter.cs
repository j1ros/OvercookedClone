using System.Collections.Generic;
using UnityEngine;
using Overcooked.InteractivObject;
using Overcooked.General;

namespace Overcooked.Counter
{
    public class BaseCounter : MonoBehaviour, ICounter
    {
        [SerializeField] protected GameObject _counterSelected;
        [SerializeField] protected Transform _placeForInteractiveObj;
        protected InteractiveObject _interactiveObject;

        protected void Awake()
        {
            EventManager.StartListening(EventType.SelectCounter, SelectCounter);
        }

        protected void OnDestroy()
        {
            EventManager.StopListening(EventType.SelectCounter, SelectCounter);
        }

        private void SelectCounter(Dictionary<EventMessageType, object> message)
        {
            if ((message[EventMessageType.Counter] as BaseCounter) == this)
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
                else if (_interactiveObject is IUnited)
                {
                    if ((_interactiveObject as IUnited).AddInteractiveObject(interactiveObj.InteractiveSO))
                    {
                        ObjectManager.Instance.DestroyInteractiveObject(interactiveObj);
                        return null;
                    }

                    if ((_interactiveObject as IUnited).AddInteractiveObject((interactiveObj as IUnited)?.PlacedInteractiveObject?.InteractiveSO))
                    {
                        (interactiveObj as IUnited).Clear();
                        return interactiveObj;
                    }
                }
                else if (interactiveObj is IUnited)
                {
                    if ((interactiveObj as IUnited).AddInteractiveObject(_interactiveObject.InteractiveSO))
                    {
                        ObjectManager.Instance.DestroyInteractiveObject(_interactiveObject);
                        PlaceInteractiveObj(interactiveObj);
                        return null;
                    }
                }
            }

            return interactiveObj;
        }

        public virtual bool CanAction()
        {
            return false;
        }

        protected virtual void PlaceInteractiveObj(InteractiveObject interactiveObj)
        {
            if (interactiveObj == null)
                return;
            _interactiveObject = interactiveObj;
            interactiveObj.gameObject.transform.SetParent(_placeForInteractiveObj, false);
        }

        public virtual void Action()
        {
            return;
        }

        public virtual void StopAction()
        {
            return;
        }
    }
}
