using System.Collections.Generic;
using Overcooked.Level;
using UnityEngine;

namespace Overcooked.GlobalMap
{
    public class LevelPoint : MonoBehaviour
    {
        [SerializeField] private LevelSO _levelData;
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
            //-- проверить на кол-во звезд
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
