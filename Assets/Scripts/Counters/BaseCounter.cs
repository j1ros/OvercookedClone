using System.Collections.Generic;
using UnityEngine;

namespace Overcooked.Counter
{
    public class BaseCounter : MonoBehaviour, IInterapt
    {
        [SerializeField] protected GameObject _counterSelected;

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

        public virtual void Interapt()
        {
            Debug.Log("Interapt in base counter");
        }
    }
}
