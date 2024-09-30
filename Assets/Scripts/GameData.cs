using System.Collections.Generic;
using Overcooked.Level;
using UnityEngine;

namespace Overcooked
{
    [CreateAssetMenu(menuName = "GameData")]
    public class GameData : ScriptableObject
    {
        [SerializeField] private string _saveFileName;
        private Dictionary<LevelSO, int> _levelRecords;
        public string SaveFileName => _saveFileName;
        public Dictionary<LevelSO, int> LevelRecords => _levelRecords;

        public void LoadSaveData(string saveFileName, bool isNewGame)
        {
            _saveFileName = saveFileName;

            if (isNewGame)
            {
                EventManager.TriggerEvent(EventType.Save, null);
            }
            else
            {
                EventManager.TriggerEvent(EventType.Load, null);
            }
        }

        public void LevelEnd(LevelSO levelSO, int points)
        {
            //-- вернуть обратно когда протестирую с положительными очками
            // if (_levelRecords[levelSO] < points)
            // {
                _levelRecords[levelSO] = points;
                EventManager.TriggerEvent(EventType.Save, null);
            // }
        }

        public void ChangeLevelRecords(Dictionary<LevelSO, int> levelRecors)
        {
            _levelRecords = levelRecors;
        }

        public void LoadLevelFromAssets()
        {
            if (_levelRecords == null)
                _levelRecords = new Dictionary<LevelSO, int>();

            LevelSO[] allLevels = Resources.LoadAll<LevelSO>("Levels/");
            for (int i = 0; i < allLevels.Length; i++)
            {
                _levelRecords[allLevels[i]] = 0;
            }
        }
    }
}
