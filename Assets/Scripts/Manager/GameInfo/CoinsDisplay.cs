using TMPro;
using UnityEngine;

namespace Orchard.GameSpace
{
    public class CoinsDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _tmpCountCoins;

        private void Render(int count)
        {
            _tmpCountCoins.text = count.ToString();
        }
        private void Awake()
        {
            GameManager.GameInfo.Coins.OnChange += Render;

            Render(GameManager.GameInfo.Coins.Value);
        }

        private void OnDisable()
        {
            GameManager.GameInfo.Coins.OnChange -= Render;
        }
    }
}
