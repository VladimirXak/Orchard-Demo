using Orchard.Game;
using Orchard.GameSpace;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Orchard
{
    public class SoundMatch : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private List<AudioClip> _listAudioClip;
        [Space(10)]
        [SerializeField] private ActiveSound _activSound;

        public static SoundMatch Instance { get; private set; }

        private int _matchNumber;
        private int MatchNumber
        {
            get
            {
                return _matchNumber;
            }
            set
            {
                if (value >= _listAudioClip.Count - 1)
                    _matchNumber = 0;
                else
                    _matchNumber = value;
            }
        }

        private DateTime _dateLastMatch;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void PlaySoundMatch()
        {
            if ((DateTime.Now - _dateLastMatch).TotalSeconds > 1)
                MatchNumber = 0;

            _audioSource.PlayOneShot(_listAudioClip[MatchNumber]);

            _dateLastMatch = DateTime.Now;
            MatchNumber++;
        }

        public void PlayClip(TypeBoardObject type, AudioClip clip)
        {
            if (!GameManager.Audio.IsPlaySound)
                return;

            if (type == TypeBoardObject.PieceRnd)
            {
                PlaySoundMatch();
                return;
            }

            if (clip != null && _activSound.AddSound(type))
            {
                _audioSource.PlayOneShot(clip);
            }
        }
    }
}
