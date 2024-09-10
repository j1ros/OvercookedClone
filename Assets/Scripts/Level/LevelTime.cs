using Overcooked.UI;
using UnityEngine;

namespace Overcooked.Level
{
    public class LevelTime : MonoBehaviour
    {
        private TimerUI _timerUI;
        private float _time;
        [HideInInspector] public float LevelTimer;

        private void Start()
        {
            _timerUI = FindObjectOfType<TimerUI>();
        }

        private void Update()
        {
            _time += Time.deltaTime;
            _timerUI.ChangeTime(LevelTimer - _time);

            if (LevelTimer <= _time)
            {
                EventManager.TriggerEvent(EventType.TimeEnd, null);
            }
        }
    }
}
