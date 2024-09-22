using System.Collections.Generic;
using UnityEngine;
using Overcooked.InteractivObject;

namespace Overcooked.Counter
{
    public class BaseCounter : MonoBehaviour, ICounter
    {
        [SerializeField] protected GameObject _counterSelected;

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
            return interactiveObj;
        }

        public virtual bool CanAction()
        {
            return false;
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
