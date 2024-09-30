using System.Collections.Generic;
using Overcooked.UI;
using UnityEngine;

namespace Overcooked.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelSO _levelSO;
        [SerializeField] private GameData _gameData;
        private LevelTime _levelTime;
        private PointsUI _pointsUI;
        private int _points = 0;
        public int Points => _points;
        public LevelSO LevelSO => _levelSO;

        private void Awake()
        {
            _levelTime = GetComponent<LevelTime>();
            _levelTime.LevelTimer = _levelSO.LevelTimer;
            _pointsUI = FindObjectOfType<PointsUI>();
            EventManager.StartListening(EventType.AddPoints, ChangePoints);
        }

        public void TimeEnd()
        {
            _gameData.LevelEnd(_levelSO, _points);
        }

        private void ChangePoints(Dictionary<EventMessageType, object> message)
        {
            _points += (int)(_levelSO.RewardForOrder * (float)message[EventMessageType.Points]);
            _pointsUI.ChangePoints(_points);
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventType.AddPoints, ChangePoints);
        }
    }
}
