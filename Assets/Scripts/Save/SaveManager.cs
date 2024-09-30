using System.Collections.Generic;
using Overcooked.Level;
using UnityEngine;

namespace Overcooked.Save
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            EventManager.StartListening(EventType.Save, SaveData);
            EventManager.StartListening(EventType.Load, LoadSave);
        }

        private void Start()
        {
            _gameData.LoadLevelFromAssets();
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventType.Save, SaveData);
            EventManager.StopListening(EventType.Load, LoadSave);
        }

        public void SaveData(Dictionary<EventMessageType, object> msg)
        {
            ContainerSaveData saveData = new ContainerSaveData(_gameData);
            SaveIO.SaveData(_gameData.SaveFileName, saveData);
        }

        public void LoadSave(Dictionary<EventMessageType, object> msg)
        {
            ContainerSaveData loadedData = SaveIO.LoadSave(_gameData.SaveFileName);
            if (loadedData == null)
                return;

            Dictionary<LevelSO, int> levelRecords = new Dictionary<LevelSO, int>();

            foreach (LevelSO level in _gameData.LevelRecords.Keys)
            {
                for (int i = 0; i < loadedData.LevelId.Length; i++)
                {
                    if (loadedData.LevelId[i] == level.ID)
                    {
                        levelRecords[level] = loadedData.LevelRecord[i];
                    }
                }
            }
            _gameData.ChangeLevelRecords(levelRecords);
        }
    }
}
