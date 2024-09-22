using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Overcooked.UI
{
    public class GameMenuUI : MonoBehaviour
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

        public void Restart()
        {
            string levelName = "";
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name != "GameScene")
                    levelName = SceneManager.GetSceneAt(i).name;
            }
            EventManager.TriggerEvent(EventType.LoadScene, new Dictionary<EventMessageType, object> { { EventMessageType.SceneName, levelName } });
        }

        public void ExitFromLevel()
        {
            EventManager.TriggerEvent(EventType.LoadScene, new Dictionary<EventMessageType, object> { { EventMessageType.SceneName, "GlobalMap" } });
        }
    }
}
