using UnityEngine;
using TMPro;

namespace Orchard.Game
{
    public class MovesMatch : MonoBehaviour
    {
        [SerializeField] private EndingLevel _endingLevel;
        [SerializeField] private BoardTapController _boardTapController;
        [SerializeField] private TextMeshProUGUI _tmpCountMoves;
        public bool IsAvailableMove { get; private set; }
        public SecureInt Value { get; private set; }

        public void Init(int newValue, bool isActivTap = false)
        {
            Value = newValue;
            _tmpCountMoves.text = Value.ToString();

            if (isActivTap)
                _boardTapController.IsCanTap = true;
        }

        private void ChangeMove()
        {
            if (Value != 0)
            {
                Value--;
                _tmpCountMoves.text = Value.ToString();

                if (Value == 0)
                {
                    _endingLevel.EndLevel();
                }
            }
        }

        private void OnEnable()
        {
            _boardTapController.OnMovesChange += ChangeMove;
        }

        private void OnDisable()
        {
            _boardTapController.OnMovesChange -= ChangeMove;
        }
    }
}
