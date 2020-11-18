using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orchard.GameSpace
{
    public class Audio : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSourceMusic;
        [SerializeField] private AudioSource _audioSourceSound;
        [Space(10)]
        [SerializeField] private List<DataAudioClipTypeSound> _listDataAudioClipTypeSounds;

        public bool IsPlayMusic { get; private set; }
        public bool IsPlaySound { get; private set; }

        private AudioActivitySettings _audioActivitySettings;

        private void Awake()
        {
            _audioActivitySettings = new AudioActivitySettings();

            IsPlayMusic = _audioActivitySettings.GetStatusMusic();
            IsPlaySound = _audioActivitySettings.GetStatusSound();
        }

        public void PlayMusic()
        {
            if (IsPlayMusic)
            {
                if (!_audioSourceMusic.isPlaying)
                    _audioSourceMusic.Play();
            }
            else
                _audioSourceMusic.Stop();
        }

        public void PlaySound(TypeSound typeSound)
        {
            if (!IsPlaySound)
                return;

            AudioClip audioClip = _listDataAudioClipTypeSounds.Find(item => item.Type == typeSound).AudioClip;

            if (audioClip != null)
                _audioSourceSound.PlayOneShot(audioClip);
        }

        public void PlaySound(AudioClip audioClip)
        {
            if (!IsPlaySound)
                return;

            _audioSourceSound.PlayOneShot(audioClip);
        }

        public void ChangeStatusMusic()
        {
            IsPlayMusic = !IsPlayMusic;

            _audioActivitySettings.ChangeStatusMusic(IsPlayMusic);

            PlayMusic();
        }

        public void ChangeStatusSound()
        {
            IsPlaySound = !IsPlaySound;

            _audioActivitySettings.ChangeStatusSound(IsPlaySound);
        }
    }

    [System.Serializable]
    public class DataAudioClipTypeSound
    {
        [SerializeField] private TypeSound _type;
        [SerializeField] private AudioClip _audioClip;

        public TypeSound Type { get => _type; set => _type = value; }
        public AudioClip AudioClip { get => _audioClip; set => _audioClip = value; }
    }
}
