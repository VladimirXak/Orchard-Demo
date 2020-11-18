using Orchard.GameSpace;
using Orchard.UI;
using System.Collections;
using UnityEngine;

namespace Orchard.Game
{
    public class EndingLevel : MonoBehaviour
    {
        [SerializeField] private Board _board;
        [SerializeField] private BoardTapController _boardTapController;
        [SerializeField] private ShufflePieces _shufflePieces;

        [SerializeField] private LevelInformation _levelInformation;

        [SerializeField] private LevelCompletedWindow _prefLevelCompletedWindow;
        [SerializeField] private LevelFailedWindow _prefLevelFailedWindow;

        private Coroutine _coroutineWaitEndAction;

        private Transform _trCanvas;

        private void Awake()
        {
            _trCanvas = FindObjectOfType<Canvas>().transform;
        }

        public void EndLevel(bool isTaskComplete = false)
        {
            if (_coroutineWaitEndAction != null)
                StopCoroutine(_coroutineWaitEndAction);

            _coroutineWaitEndAction = StartCoroutine(WaitEndAction(isTaskComplete));
        }

        private IEnumerator WaitEndAction(bool isTaskComplete)
        {
            _boardTapController.IsCanTap = false;

            yield return new WaitForSeconds(2f);

            while (_board.ActionTiles.Count > 0)
                yield return new WaitForSeconds(0.5f);

            _shufflePieces.StopHint();

            if (isTaskComplete && _levelInformation.CheckCompleteTask())
            {
                int rewardCoin = 0;

                if (GameManager.GameInfo.NumberLevel == _levelInformation.NumberLevel)
                {
                    rewardCoin = 50 + _levelInformation.MovesMatch.Value;
                    GameManager.GameInfo.LevelCompleted(false);
                }
                else
                    rewardCoin = 15;

                GameManager.GameInfo.Health.AddHealth(1, GameManager.GameInfo.Health.DateUpdate, false);
                GameManager.GameInfo.Coins.AddCoins(rewardCoin);

                var levelCompletedPanel = Instantiate(_prefLevelCompletedWindow, _trCanvas);
                levelCompletedPanel.Init(_levelInformation.NumberLevel, rewardCoin);
            }
            else
            {
                var levelFailedPanel = Instantiate(_prefLevelFailedWindow, _trCanvas);
                levelFailedPanel.Init(_levelInformation, _board.HideBoard);
            }
        }
    }
}
