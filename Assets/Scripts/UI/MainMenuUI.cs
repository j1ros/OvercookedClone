using System.Collections.Generic;
using UnityEngine;

namespace Overcooked.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject _saveSlotsPanel;
        [SerializeField] private GameData _gameData;
        private bool _newGameStart;

        public void ChooseSaveSlot(bool newGameStart)
        {
            _newGameStart = newGameStart;
            _saveSlotsPanel.gameObject.SetActive(true);
        }

        public void StartGame(string saveFileName)
        {
            _gameData.LoadSaveData(saveFileName, _newGameStart);
            EventManager.TriggerEvent(EventType.LoadScene, new Dictionary<EventMessageType, object> { { EventMessageType.SceneName, "GlobalMap" } });
        }
    }
}
