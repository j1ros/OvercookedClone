using System.Collections.Generic;
using UnityEngine;

namespace Overcooked.UI
{
    public class GlobalMapUI : MonoBehaviour
    {
        public void LoadLevel(string level)
        {
            EventManager.TriggerEvent(EventType.LoadScene, new Dictionary<EventMessageType, object> { { EventMessageType.SceneName, level } });
        }
    }
}
