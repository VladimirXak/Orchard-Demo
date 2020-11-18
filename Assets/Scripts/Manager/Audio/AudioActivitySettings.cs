using UnityEngine;
using System;

namespace Orchard.GameSpace
{
    public class AudioActivitySettings
    {
        private const string _keyMusic = "Music";
        private const string _keySound = "Sound";

        public bool GetStatusMusic()
        {
            return GetStatusAudio(_keyMusic);
        }

        public bool GetStatusSound()
        {
            return GetStatusAudio(_keySound);
        }

        public void ChangeStatusMusic(bool value)
        {
            SaveStatusAudio(_keyMusic, value);
        }

        public void ChangeStatusSound(bool value)
        {
            SaveStatusAudio(_keySound, value);
        }

        private bool GetStatusAudio(string key)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.SetInt(key, 1);
                return true;
            }

            return Convert.ToBoolean(PlayerPrefs.GetInt(key));
        }

        private void SaveStatusAudio(string key, bool value)
        {
            PlayerPrefs.SetInt(key, Convert.ToInt32(value));
        }
    }
}
