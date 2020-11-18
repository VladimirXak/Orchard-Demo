using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Orchard.UI
{
    [RequireComponent(typeof(Image))]
    public class SceneTransitionWindow : MonoBehaviour
    {
        private Image _imgBlackPanel;

        private void Awake()
        {
            _imgBlackPanel = GetComponent<Image>();
        }

        public void Show(int buildIndexScene)
        {
            Color color = _imgBlackPanel.color;
            color.a = 0;

            _imgBlackPanel.color = color;
            _imgBlackPanel.gameObject.SetActive(true);

            DOTween.KillAll();

            _imgBlackPanel.DOColor(new Color(color.r, color.g, color.b, 1), 0.5f).OnComplete(delegate
            {
                SceneManager.LoadScene(buildIndexScene);
            });
        }

        public void Hide()
        {
            Color color = _imgBlackPanel.color;
            color.a = 1;

            _imgBlackPanel.color = color;
            _imgBlackPanel.gameObject.SetActive(true);

            _imgBlackPanel.DOColor(new Color(color.r, color.g, color.b, 0), 0.5f).OnComplete(delegate
            {
                Destroy(gameObject);
            });
        }
    }
}
