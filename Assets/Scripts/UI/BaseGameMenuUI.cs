using System.Collections.Generic;
using UnityEngine;

namespace Overcooked.UI
{
    public class BaseGameMenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject _menu;

        private void Awake()
        {
            EventManager.StartListening(EventType.Menu, OpenCloseMenu);
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventType.Menu, OpenCloseMenu);
        }

        private void OpenCloseMenu(Dictionary<EventMessageType, object> data)
        {
            if (_menu.gameObject.activeSelf)
            {
                Resume();
            }
            else
            {
                EventManager.TriggerEvent(EventType.Pause, null);
                _menu.gameObject.SetActive(true);
            }
        }

        public void Resume()
        {
            EventManager.TriggerEvent(EventType.Unpause, null);
            _menu.gameObject.SetActive(false);
        }

        public virtual void ExitFromLevel()
        {
            EventManager.TriggerEvent(EventType.LoadScene, new Dictionary<EventMessageType, object> { { EventMessageType.SceneName, "MainMenu" } });
        }
    }
}
