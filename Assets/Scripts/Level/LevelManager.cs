using System.Collections.Generic;
using Overcooked.UI;
using UnityEngine;

namespace Overcooked.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelSO _levelSO;
        private LevelTime _levelTime;
        private PointsUI _pointsUI;
        private int _points = 0;
        public LevelSO LevelSO => _levelSO;

        private void Awake()
        {
            _levelTime = GetComponent<LevelTime>();
            _levelTime.LevelTimer = _levelSO.LevelTimer;
            _pointsUI = FindObjectOfType<PointsUI>();
            EventManager.StartListening(EventType.AddPoints, ChangePoints);
        }

        private void ChangePoints(Dictionary<EventMessageType, object> message)
        {
            _points += (int)message[EventMessageType.Points];
            _pointsUI.ChangePoints(_points);
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventType.AddPoints, ChangePoints);
        }
    }
}
