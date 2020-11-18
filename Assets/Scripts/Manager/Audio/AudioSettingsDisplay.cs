using UnityEngine;
using UnityEngine.UI;
using Orchard.GameSpace;

namespace Orchard.UI
{
    public class AudioSettingsDisplay : MonoBehaviour
    {
        [SerializeField] private SwitchTwoObject _switchTwoObjectMusic;
        [SerializeField] private SwitchTwoObject _switchTwoObjectSound;
        [Space(10)]
        [SerializeField] private Button _switchMusicButton;
        [SerializeField] private Button _switchSoundButton;

        private Audio _audio;

        private void Awake()
        {
            _switchMusicButton.onClick.AddListener(ChangeStatusMusic);
            _switchSoundButton.onClick.AddListener(ChangeStatusSound);

            _audio = GameManager.Audio;

            _switchTwoObjectMusic.Switch(_audio.IsPlayMusic);
            _switchTwoObjectSound.Switch(_audio.IsPlaySound);
        }

        private void ChangeStatusMusic()
        {
            _audio.ChangeStatusMusic();
            _switchTwoObjectMusic.Switch(_audio.IsPlayMusic);
        }

        private void ChangeStatusSound()
        {
            _audio.ChangeStatusSound();
            _switchTwoObjectSound.Switch(_audio.IsPlaySound);
        }
    }
}
