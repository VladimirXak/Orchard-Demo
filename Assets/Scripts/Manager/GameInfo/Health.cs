using System;
using UnityEngine;
using UnityEngine.Events;

namespace Orchard.GameSpace
{
    public class Health
    {
        public event UnityAction<int> OnChange;
        private Action<bool> OnSaveData;

        public DateTime DateUpdate { get; private set; }
        public bool IsInfinityHealth { get; private set; }

        private SecureInt value;
        public SecureInt Value
        {
            get
            {
                return value;
            }
            private set
            {
                if (value < 0)
                    value = 0;

                this.value = value;
                OnChange?.Invoke(value);
            }
        }

        public void Init(JsonDataSavedHealth dataHealth, Action<bool> saveData)
        {
            OnSaveData = saveData;

            DateUpdate = DateTime.FromFileTime(dataHealth.dateUpdate);

            value = dataHealth.count;
            IsInfinityHealth = dataHealth.isInfinity;

            if (!SetHealth())
                OnChange?.Invoke(Value);
        }

        public void AddHealth(int countHealth, DateTime newDate, bool isSave = true)
        {
            if (TryStopInfinityHealth(false) || IsInfinityHealth)
                return;

            DateUpdate = newDate;
            Value += countHealth;

            OnSaveData?.Invoke(isSave);
        }

        public void AddInfinityHealth(int countMinutes, bool isSave = true)
        {
            if (Value < GameManager.Config.MAX_HEALTH)
                value = GameManager.Config.MAX_HEALTH;

            if (IsInfinityHealth)
                DateUpdate = DateUpdate.AddMinutes(countMinutes);
            else
            {
                IsInfinityHealth = true;
                DateUpdate = DateTime.Now.AddMinutes(countMinutes);
            }

            OnChange?.Invoke(Value);

            OnSaveData?.Invoke(isSave);
        }

        public JsonDataSavedHealth GetDataHealth()
        {
            JsonDataSavedHealth dataSavedHealth = new JsonDataSavedHealth()
            {
                count = Value,
                dateUpdate = DateUpdate.ToFileTime(),
                isInfinity = IsInfinityHealth
            };

            return dataSavedHealth;
        }

        public void DecreaseHealth(bool isSave = true)
        {
            TryStopInfinityHealth(false);

            if (IsInfinityHealth)
                return;

            if (Value == GameManager.Config.MAX_HEALTH)
                DateUpdate = DateTime.Now.AddMinutes(30);

            Value--;

            OnSaveData?.Invoke(isSave);
        }

        public bool TryStopInfinityHealth(bool isSave = true)
        {
            if (IsInfinityHealth && DateTime.Now > DateUpdate)
            {
                IsInfinityHealth = false;
                OnSaveData?.Invoke(isSave);

                return true;
            }

            return false;
        }

        private bool SetHealth()
        {
            int maxHealth = GameManager.Config.MAX_HEALTH;

            if (Value < maxHealth)
            {
                int addHealth = GetCountAddHealth(DateUpdate);

                if (addHealth > 0)
                {
                    DateTime newDateHealth = DateUpdate.AddMinutes(GameManager.Config.TIME_RECOVERY_HEALTH * addHealth);

                    if (Value + addHealth > maxHealth)
                    {
                        addHealth = Mathf.Clamp(maxHealth - Value, 0, maxHealth);
                    }

                    AddHealth(addHealth, newDateHealth);

                    return true;
                }
            }

            return false;
        }

        private int GetCountAddHealth(DateTime dateHealth)
        {
            TimeSpan timeSpan = DateTime.Now - dateHealth;

            int addHealth = 0;

            if (timeSpan.TotalSeconds > 0)
            {
                addHealth++;

                return addHealth + Mathf.FloorToInt((float)timeSpan.TotalMinutes / GameManager.Config.TIME_RECOVERY_HEALTH);
            }

            return 0;
        }
    }
}
