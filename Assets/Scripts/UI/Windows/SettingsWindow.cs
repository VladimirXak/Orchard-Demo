using UnityEngine;
using UnityEngine.UI;

namespace Orchard.UI
{
    public class SettingsWindow : MonoBehaviour
    {
        [SerializeField] private SelectLanguageWindow _prefSelectLanguageWindow;
        [SerializeField] private Button _selectLanguageButton;

        private void Awake()
        {
            _selectLanguageButton.onClick.AddListener(OpenLanguageWindow);
        }

        private void OpenLanguageWindow()
        {
            Instantiate(_prefSelectLanguageWindow, FindObjectOfType<Canvas>().transform).Init();
        }
    }
}
