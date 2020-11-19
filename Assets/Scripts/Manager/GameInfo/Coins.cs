using System;
using UnityEngine.Events;

namespace Orchard.GameSpace
{
    public class Coins
    {
        public event UnityAction<int> OnChange;

        private Action<bool> OnSaveData;

        private SecureInt _value;
        public SecureInt Value
        {
            get
            {
                return _value;
            }
            private set
            {
                _value = value;
                OnChange?.Invoke(value);
            }
        }

        public Coins(int coins, Action<bool> saveData)
        {
            _value = coins;
            OnSaveData = saveData;
        }

        public void AddCoins(int countCoins, bool isSave = true)
        {
            Value += countCoins;
            OnSaveData(isSave);
        }

        public bool TryBuy(int price, bool isSave = true)
        {
            if (Value >= price)
            {
                Buy(price, isSave);
                return true;
            }

            return false;
        }

        private void Buy(int price, bool isSave)
        {
            Value -= price;
            OnSaveData(isSave);
        }
    }
}
