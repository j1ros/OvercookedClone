using System.Collections.Generic;
using UnityEngine;

namespace Overcooked.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public void StartGame(string nameScene)
        {
            EventManager.TriggerEvent(EventType.LoadScene, new Dictionary<EventMessageType, object> { { EventMessageType.SceneName, nameScene } });
        }
    }
}
