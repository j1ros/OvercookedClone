using System.Collections.Generic;
using UnityEngine;
using Overcooked.UI;

namespace Overcooked.General
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private GameEndUI _gameEndUI;

        private void Awake()
        {
            EventManager.StartListening(EventType.TimeEnd, LevelEnd);
            EventManager.StartListening(EventType.Pause, PauseGame);
            EventManager.StartListening(EventType.Unpause, UnpauseGame);
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventType.TimeEnd, LevelEnd);
            EventManager.StopListening(EventType.Pause, PauseGame);
            EventManager.StopListening(EventType.Unpause, UnpauseGame);
        }

        private void LevelEnd(Dictionary<EventMessageType, object> data)
        {
            List<int> pointStars = data[EventMessageType.PointStars] as List<int>;
            int points = (int)data[EventMessageType.Points];
            PauseGame();
            _gameEndUI.SetPoints(pointStars, points);
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
        }

        private void UnpauseGame()
        {
            Time.timeScale = 1;
        }

        private void PauseGame(Dictionary<EventMessageType, object> data)
        {
            PauseGame();
        }

        private void UnpauseGame(Dictionary<EventMessageType, object> data)
        {
            UnpauseGame();
        }
    }
}
