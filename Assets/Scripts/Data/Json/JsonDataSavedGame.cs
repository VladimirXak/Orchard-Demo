using System;

namespace Orchard.GameSpace
{
    [Serializable]
    public struct JsonDataSavedGame
    {
        public long dateSaved;
        public int numberLevel;
        public int countCoins;

        public JsonDataSavedHealth dataSavedHealth;
    }

    [Serializable]
    public struct JsonDataSavedHealth
    {
        public int count;
        public long dateUpdate;
        public bool isInfinity;
    }
}
