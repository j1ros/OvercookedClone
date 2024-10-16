using System.Collections.Generic;
using Overcooked.Level;
using TMPro;
using UnityEngine;

namespace Overcooked.GlobalMap
{
    public class LevelPointUI : MonoBehaviour
    {
        [SerializeField] private GameObject _ui;
        [SerializeField] private TextMeshProUGUI _levelNameText;
        [SerializeField] private TextMeshProUGUI _levelRecordText;
        [SerializeField] private TextMeshProUGUI _requireStars;
        [SerializeField] private GameData _gameData;

        private void Awake()
        {
            EventManager.StartListening(EventType.LevelPointUI, ChangeUI);
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventType.LevelPointUI, ChangeUI);
        }

        private void ChangeUI(Dictionary<EventMessageType, object> data)
        {
            if (data == null)
            {
                HideUI();
            }
            else if (data[EventMessageType.LevelSO] as LevelSO != null)
            {
                ShowUI(data[EventMessageType.LevelSO] as LevelSO);
            }
        }

        private void HideUI()
        {
            _ui.SetActive(false);
            _requireStars.gameObject.SetActive(false);
            _levelRecordText.gameObject.SetActive(false);
        }

        private void ShowUI(LevelSO levelData)
        {
            _levelNameText.text = levelData.NameScene;
            if (_gameData.GetStars() >= levelData.StarForUnlock)
            {
                _levelRecordText.gameObject.SetActive(true);
                _levelRecordText.text = _gameData.LevelRecords[levelData].ToString();
            }
            else
            {
                _requireStars.gameObject.SetActive(true);
                _requireStars.text = "Необходимо " + levelData.StarForUnlock + " для открытия";
            }
            _ui.SetActive(true);
        }
    }
}
