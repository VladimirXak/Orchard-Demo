using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Orchard.GameSpace
{
    public class Localization : MonoBehaviour
    {
        private Dictionary<string, string> _listLocalizationValues = new Dictionary<string, string>();

        private const string _keyLanguage = "language";

        public int IdLanguage { get; private set; }

        public void Init()
        {
            SetLanguage();
        }

        private void SetLanguage()
        {
            if (!PlayerPrefs.HasKey(_keyLanguage))
            {
                if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Belarusian)
                {
                    PlayerPrefs.SetInt(_keyLanguage, (int)SystemLanguage.Russian);
                }
                else
                {
                    PlayerPrefs.SetInt(_keyLanguage, (int)SystemLanguage.English);
                }
            }

            ChangeLanguage(PlayerPrefs.GetInt(_keyLanguage));
        }

        public void ChangeLanguage(int idLanguage)
        {
            IdLanguage = idLanguage;

            _listLocalizationValues.Clear();

            SystemLanguage language = (SystemLanguage)idLanguage;

            TextAsset txtLocalization = Resources.Load<TextAsset>($"Localization/{language}") as TextAsset;

            if (txtLocalization != null)
            {
                DataLocalization dataLocalization = JsonUtility.FromJson<DataLocalization>(txtLocalization.ToString());

                foreach (var dataItemLocalization in dataLocalization.listDataItems)
                {
                    _listLocalizationValues.Add(dataItemLocalization.key, dataItemLocalization.value);
                }
            }

            var items = FindObjectsOfType<MonoBehaviour>().OfType<ILocalizationItem>();

            foreach (var item in items)
            {
                if (_listLocalizationValues.ContainsKey(item.Key))
                {
                    item.ChangeLocalization(_listLocalizationValues[item.Key]);
                }
            }

            PlayerPrefs.SetInt(_keyLanguage, idLanguage);
        }

        public string GetValue(string key)
        {
            if (_listLocalizationValues.ContainsKey(key))
            {
                return _listLocalizationValues[key];
            }

            return null;
        }
    }
}
