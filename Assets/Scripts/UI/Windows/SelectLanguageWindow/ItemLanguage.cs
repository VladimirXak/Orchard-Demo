using Orchard.GameSpace;
using UnityEngine;
using UnityEngine.UI;

namespace Orchard.UI
{
    [RequireComponent(typeof(Button))]
    public class ItemLanguage : MonoBehaviour
    {
        [SerializeField] private SystemLanguage _language;

        private Button _selectLanguageButton;

        public void Init(SelectLanguageWindow selectLanguageWindow)
        {
            _selectLanguageButton = GetComponent<Button>();

            _selectLanguageButton.onClick.AddListener(() =>
            {
                SelectLanguage();
                selectLanguageWindow.Hide();
            });
        }

        private void SelectLanguage()
        {
            GameManager.Localization.ChangeLanguage((int)_language);
        }
    }
}
