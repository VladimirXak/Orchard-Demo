using System;
using TMPro;
using UnityEngine;

namespace Orchard.GameSpace
{
    public class HealthDisplay : MonoBehaviour, ILocalizationItem
    {
        [SerializeField] private TextMeshProUGUI _tmpCountHealth;
        [SerializeField] private TextMeshProUGUI _tmpInfinityHealth;
        [SerializeField] private TextMeshProUGUI _tmpTimer;
        [SerializeField] private Timer _timer;

        private Health _health;

        public string Key { get; private set; } = "All";

        private void Awake()
        {
            _health = GameManager.GameInfo.Health;

            _health.OnChange += Render;
            _timer.OnEndTime += EndTimer;
            _timer.OnTickTimer += TickTimer;

            Render(_health.Value);
        }

        private void Render(int count)
        {
            _tmpCountHealth.text = count.ToString();

            if (_health.Value < GameManager.Config.MAX_HEALTH)
            {
                _timer.StartTimer(_health.DateUpdate);
            }
            else if (_health.IsInfinityHealth)
            {
                IsInfinityHealthRender(true);
                _timer.StartTimer(_health.DateUpdate);
            }
            else
            {
                _timer.Stop();
                _tmpTimer.text = GameManager.Localization.GetValue(Key);
            }
        }

        private void IsInfinityHealthRender(bool isInfinity)
        {
            _tmpInfinityHealth.gameObject.SetActive(isInfinity);
            _tmpCountHealth.gameObject.SetActive(!isInfinity);
        }

        private void EndTimer()
        {
            if (_health.IsInfinityHealth)
            {
                IsInfinityHealthRender(false);
                _tmpTimer.text = GameManager.Localization.GetValue(Key);
            }
            else
            {
                DateTime nextHealth = DateTime.Now.AddMinutes(30);

                _health.AddHealth(1, nextHealth, true);
            }
        }

        private void TickTimer(string result)
        {
            _tmpTimer.text = result;
        }

        private void OnDisable()
        {
            GameManager.GameInfo.Health.OnChange -= Render;
            _timer.OnEndTime -= EndTimer;
            _timer.OnTickTimer -= TickTimer;
        }

        public void ChangeLocalization(string value)
        {
            _tmpTimer.text = value;
        }
    }
}
