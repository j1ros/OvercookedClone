using System.Collections.Generic;
using Overcooked.UI;
using UnityEngine;

namespace Overcooked.Level
{
    public class LevelTime : MonoBehaviour
    {
        [SerializeField] private LevelManager _levelManager;
        private TimerUI _timerUI;
        private float _time;
        private bool _isSubmitEvent;
        public float TimeFromStart => _time;
        [HideInInspector] public float LevelTimer;

        private void Start()
        {
            _isSubmitEvent = false;
            _timerUI = FindObjectOfType<TimerUI>();
        }

        private void Update()
        {
            _time += Time.deltaTime;
            _timerUI.ChangeTime(LevelTimer - _time);

            if (LevelTimer <= _time && !_isSubmitEvent)
            {
                EventManager.TriggerEvent(EventType.TimeEnd, new Dictionary<EventMessageType, object> { { EventMessageType.PointStars, _levelManager.LevelSO.PointsForStars },
                    { EventMessageType.Points,_levelManager.Points} });
                _levelManager.TimeEnd();
                _isSubmitEvent = true;
            }
        }
    }
}
