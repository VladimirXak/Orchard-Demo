using UnityEngine;
using UnityEngine.UI;

namespace Orchard.UI
{
    public class TermsWindow : Window
    {
        [Space(10)]
        [SerializeField] private Button _openUrlTermsButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private string _urlTerms;

        private string _keyTerms;

        public void Init(string keyTerms)
        {
            _openUrlTermsButton.onClick.AddListener(OpenUrlTerms);
            _continueButton.onClick.AddListener(Hide);

            this._keyTerms = keyTerms;
            AnimationShowWindow();
        }

        public override void Hide()
        {
            PlayerPrefs.SetInt(_keyTerms, 1);
            PlayerPrefs.Save();
            base.Hide();
        }

        private void OpenUrlTerms()
        {
            Application.OpenURL(_urlTerms);
        }
    }
}
