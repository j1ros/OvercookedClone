using System.Collections.Generic;
using System.Linq;
using Overcooked.Level;
using System;

namespace Overcooked.Save
{
    [Serializable]
    public class ContainerSaveData
    {
        public int[] LevelRecord;
        public int[] LevelId;

        public ContainerSaveData(GameData gameData)
        {
            LevelRecord = new int[gameData.LevelRecords.Count];
            LevelId = new int[gameData.LevelRecords.Count];
            List<LevelSO> levelsList = new List<LevelSO>();

            foreach (LevelSO level in gameData.LevelRecords.Keys)
            {
                levelsList.Add(level);
            }

            var sortedLevelList = levelsList.OrderBy(level => level.ID);

            for (int i = 0; i < sortedLevelList.Count(); i++)
            {
                LevelId[i] = sortedLevelList.ElementAt(i).ID;
                LevelRecord[i] = gameData.LevelRecords[sortedLevelList.ElementAt(i)];
            }
        }
    }
}
