using UnityEngine;

namespace Overcooked.Save
{
    public static class SaveIO
    {
        private static string _baseSavePath;

        static SaveIO()
        {
            _baseSavePath = Application.persistentDataPath;
        }

        public static void SaveData(string fileName, ContainerSaveData dataSave)
        {
            FileReadWrite.WriteToBinaryFile(_baseSavePath + "/" + fileName + ".dat", dataSave);
        }

        public static ContainerSaveData LoadSave(string fileName)
        {
            string filePath = _baseSavePath + "/" + fileName + ".dat";

            if (System.IO.File.Exists(filePath))
            {
                return FileReadWrite.ReadFromBinaryFile<ContainerSaveData>(filePath);
            }
            return null;
        }
    }
}
