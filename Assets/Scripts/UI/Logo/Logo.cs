using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Orchard.UI.Logo
{
    [RequireComponent(typeof(Image))]
    public class Logo : MonoBehaviour
    {
        [SerializeField] private TermsWindow _prefTermsWindow;

        private Image _logo;
        private Transform _canvas;
        private const string _keyTerms = "Terms";

        private void Awake()
        {
            SetApplicationSettings();

            _canvas = FindObjectOfType<Canvas>().transform;
            _logo = GetComponent<Image>();

            _logo.SetAlpha(0);

            _logo.DOFade(1, 1.5f).SetEase(Ease.Linear).SetDelay(0.5f).OnComplete(delegate
            {
                StartCoroutine(CoroutineWaitTermsWindow());
            });
        }

        private IEnumerator CoroutineWaitTermsWindow()
        {
            TermsWindow termsWindow = null;

            if (!PlayerPrefs.HasKey(_keyTerms) || PlayerPrefs.GetInt(_keyTerms) == 0)
            {
                termsWindow = Instantiate(_prefTermsWindow, _canvas);
                termsWindow.Init(_keyTerms);
            }

            while (termsWindow != null)
            {
                yield return null;
            }

            _logo.DOFade(0, 1.5f).SetEase(Ease.Linear).OnComplete(delegate
            {
                SceneManager.LoadScene(1);
            });
        }

        private void SetApplicationSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 144;
        }
    }
}
