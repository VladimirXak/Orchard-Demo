using Orchard.GameSpace;
using Orchard.UI;
using UnityEngine;

namespace Orchard.Game
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private SceneTransitionWindow _prefSceneTransitionWindow;

        private void Awake()
        {
            Instantiate(_prefSceneTransitionWindow, FindObjectOfType<Canvas>().transform).Hide();

            GameManager.Audio.PlayMusic();
        }
    }
}
