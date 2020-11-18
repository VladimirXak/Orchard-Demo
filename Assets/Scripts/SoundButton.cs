using Orchard.GameSpace;
using UnityEngine;
using UnityEngine.UI;

namespace Orchard.Game
{
    public class SoundButton : MonoBehaviour
    {
        public void ButtonClick()
        {
            GameManager.Audio.PlaySound(TypeSound.ButtonClick);
        }
    }
}
