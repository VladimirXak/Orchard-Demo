using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Orchard.UI
{
    public class SwitchPanel : MonoBehaviour
    {
        [SerializeField] private Transform _mainPanel;
        [SerializeField] private List<PanelForSwitch> _listPanels;
        [SerializeField] private GameObject _block;

        private Transform _activePanel;

        private Color _colorSelected = new Color(1, 0.49f, 0, 0.4f);

        private void Awake()
        {
            if (_listPanels.Count == 0)
                return;

            foreach (var panelForSwitch in _listPanels)
            {
                panelForSwitch.Panel.gameObject.SetActive(false);

                panelForSwitch.ShowPanelButton.onClick.AddListener(() =>
                {
                    if (_activePanel != panelForSwitch.Panel)
                        Show(panelForSwitch.Panel);
                });
            }

            _activePanel = _mainPanel;
            _mainPanel.gameObject.SetActive(true);
        }

        public void Show(Transform panel)
        {
            Hide(_activePanel);
            panel.gameObject.SetActive(true);

            ChangeColorImage(panel, _colorSelected);

            CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();

            _block.SetActive(true);
            panel.localScale = Vector3.zero;
            canvasGroup.alpha = 0;

            canvasGroup.DOFade(1, 0.4f);
            panel.gameObject.SetActive(true);

            panel.DOScale(1.1f, 0.3f).OnComplete(delegate
            {
                panel.DOScale(1f, 0.1f).OnComplete(delegate
                {
                    _block.SetActive(false);
                });
            });

            _activePanel = panel;
        }

        public void Hide(Transform panel)
        {
            ChangeColorImage(panel, new Color(1, 1, 1, 0));

            CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();

            _block.SetActive(true);
            canvasGroup.DOFade(0, 0.4f);

            panel.DOScale(1.1f, 0.1f).OnComplete(delegate
            {
                panel.DOScale(0, 0.3f).OnComplete(delegate
                {
                    _block.SetActive(false);
                    panel.gameObject.SetActive(false);
                });
            });
        }

        private void ChangeColorImage(Transform panel, Color color)
        {
            _listPanels.Find(v => v.Panel == panel).ShowPanelButton.GetComponent<Image>().color = color;
        }
    }

    [System.Serializable]
    public struct PanelForSwitch
    {
        public Transform Panel;
        public Button ShowPanelButton;
    }
}
