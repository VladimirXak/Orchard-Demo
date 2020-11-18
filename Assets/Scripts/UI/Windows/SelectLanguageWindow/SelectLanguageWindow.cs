using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Orchard.UI
{
    public class SelectLanguageWindow : Window
    {
        [SerializeField] private Button _closeWindowButton;
        [Space(10)]
        [SerializeField] private List<ItemLanguage> _listItemLanguage;

        public void Init()
        {
            _closeWindowButton.onClick.AddListener(Hide);

            AnimationShowWindow();

            foreach (var itemLanguage in _listItemLanguage)
            {
                itemLanguage.Init(this);
            }
        }
    }
}
