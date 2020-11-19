using System;
using UnityEngine;

namespace Orchard.GameSpace
{
    public class GameInfo : MonoBehaviour
    {
        public SecureInt NumberLevel { get; private set; }
        public Coins Coins { get; private set; }
        public Health Health { get; private set; }

        private const string _keySavedGame = "HakoOrchard";

        public void Init()
        {
            Health = new Health();

            JsonDataSavedGame dataSavedGame = LoadFromPlayerPrefs();

            NumberLevel = dataSavedGame.numberLevel;
            Coins = new Coins(dataSavedGame.countCoins, TrySaveData);
            Health.Init(dataSavedGame.dataSavedHealth, TrySaveData);
        }

        public void LevelCompleted(bool isSave = true)
        {
            NumberLevel++;
            TrySaveData(isSave);
        }

        public void TrySaveData(bool isSave = true)
        {
            if (!isSave)
                return;

            JsonDataSavedGame dataSavedGame = new JsonDataSavedGame()
            {
                dateSaved = DateTime.Now.ToFileTime(),

                numberLevel = NumberLevel,
                countCoins = Coins.Value,

                dataSavedHealth = Health.GetDataHealth(),
            };

            string dataToSave = JsonUtility.ToJson(dataSavedGame);

            PlayerPrefs.SetString(_keySavedGame, AesEncryption.Encrypt(dataToSave));
            PlayerPrefs.Save();
        }

        private JsonDataSavedGame LoadFromPlayerPrefs()
        {
            if (PlayerPrefs.HasKey(_keySavedGame))
            {
                try
                {
                    string dataSaved = PlayerPrefs.GetString(_keySavedGame);
                    return JsonUtility.FromJson<JsonDataSavedGame>(AesEncryption.Decrypt(dataSaved));
                }
                catch
                {
                    PlayerPrefs.DeleteKey(_keySavedGame);
                    return LoadFromPlayerPrefs();
                }
            }
            else
            {
                JsonDataSavedGame dataSavedGame = new JsonDataSavedGame()
                {
                    dateSaved = DateTime.Now.ToFileTime(),

                    numberLevel = 1,
                    countCoins = 1000,

                    dataSavedHealth = new JsonDataSavedHealth()
                    {
                        count = 5,
                        dateUpdate = DateTime.Now.AddMinutes(30).ToFileTime(),
                        isInfinity = false
                    },
                };

                string dataSave = JsonUtility.ToJson(dataSavedGame);

                PlayerPrefs.SetString(_keySavedGame, AesEncryption.Encrypt(dataSave));
                PlayerPrefs.Save();

                return dataSavedGame;
            }
        }
    }
}