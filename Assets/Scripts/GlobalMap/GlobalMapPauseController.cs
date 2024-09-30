using System.Collections.Generic;
using UnityEngine;

namespace Overcooked.GlobalMap
{
    public class GlobalMapPauseController : MonoBehaviour
    {
        private void Awake()
        {
            EventManager.StartListening(EventType.Pause, Pause);
            EventManager.StartListening(EventType.Unpause, Unpause);
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventType.Pause, Pause);
            EventManager.StopListening(EventType.Unpause, Unpause);
        }

        private void Pause(Dictionary<EventMessageType, object> message)
        {
            Time.timeScale = 0;
        }

        private void Unpause(Dictionary<EventMessageType, object> message)
        {
            Time.timeScale = 1;
        }
    }
}
