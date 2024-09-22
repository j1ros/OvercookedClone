using System.Collections;
using System.Collections.Generic;
using Overcooked.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Overcooked.General
{
    public class OvercookedSceneManager : MonoBehaviour
    {
        public static OvercookedSceneManager Instance;
        private List<AsyncOperation> _asyncOperation = new List<AsyncOperation>();
        [SerializeField] private LoadScreenUI _loadSceneUI;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            EventManager.StartListening(EventType.LoadScene, LoadScene);
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventType.LoadScene, LoadScene);
        }

        private void LoadScene(Dictionary<EventMessageType, object> data)
        {
            EventManager.TriggerEvent(EventType.Pause, null);
            string sceneName = data[EventMessageType.SceneName] as string;
            if (sceneName != "MainMenu" && sceneName != "GlobalMap")
            {
                _asyncOperation.Add(SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single));
                _asyncOperation.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
            }
            else
            {
                _asyncOperation.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single));
            }
            StartCoroutine(LoadProcess());
        }

        IEnumerator LoadProcess()
        {
            float totalProgress = 0f;
            _loadSceneUI.gameObject.SetActive(true);
            for (int i = 0; i < _asyncOperation.Count; i++)
            {
                while (!_asyncOperation[i].isDone)
                {
                    totalProgress += _asyncOperation[i].progress;
                    _loadSceneUI.SetProgress(totalProgress / _asyncOperation.Count);
                    yield return null;
                }
            }
            _asyncOperation.Clear();
            _loadSceneUI.gameObject.SetActive(false);
            EventManager.TriggerEvent(EventType.Unpause, null);
        }
    }
}
