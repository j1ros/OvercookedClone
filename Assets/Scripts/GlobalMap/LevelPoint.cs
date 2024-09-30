using System.Collections.Generic;
using Overcooked.Level;
using UnityEngine;

namespace Overcooked.GlobalMap
{
    public class LevelPoint : MonoBehaviour
    {
        [SerializeField] private LevelSO _levelData;
        [SerializeField] private GameData _gameData;
        private bool _isTarget = false;

        private void Awake()
        {
            EventManager.StartListening(EventType.Action, StartLevel);
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventType.Action, StartLevel);
        }

        private void StartLevel(Dictionary<EventMessageType, object> data)
        {
            if (!_isTarget)
                return;
                
            if (_gameData.GetStars() >= _levelData.StarForUnlock)
                EventManager.TriggerEvent(EventType.LoadScene, new Dictionary<EventMessageType, object> { { EventMessageType.SceneName, _levelData.NameScene } });
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<CarMovement>() != null)
            {
                _isTarget = true;
                EventManager.TriggerEvent(EventType.LevelPointUI, new Dictionary<EventMessageType, object> { { EventMessageType.LevelSO, _levelData } });
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _isTarget = false;
            EventManager.TriggerEvent(EventType.LevelPointUI, null);
        }
    }
}
