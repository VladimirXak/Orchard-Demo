using DG.Tweening;
using Orchard.GameSpace;
using System;
using UnityEngine;

namespace Orchard.Game
{
    public class SwappingPieces : MonoBehaviour
    {
        public Tile Tile { get => _piece.Tile; }
        public Tile TileLast { get => _piece.TileLast; }

        public float LastMoveTime { get; private set; }

        private Piece _piece;
        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _piece = GetComponent<Piece>();
        }

        public void SetLastMoveTime()
        {
            LastMoveTime = Time.time;
        }

        public void Swap(Action OnMovesChange, TypeSwap typeSwap, bool isMainPiece, bool isBack = false)
        {
            SwapToPieces(OnMovesChange, isMainPiece, isBack);
        }

        private void SwapToPieces(Action OnMovesChange, bool isMainPiece, bool isBack = false)
        {
            Tile.Actions.TileActivities.NewAction(() =>
            {
                _transform.DOLocalMove(Vector2.zero, GameManager.Config.TIME_SWAP).SetEase(Ease.InOutSine, GameManager.Config.TIME_SWAP).OnComplete(delegate
                {
                    TileLast.Actions.IsSwap = false;

                    Tile.Actions.TileActivities.StopAction(TypeActions.PieceMoving, false);

                    if (isBack)
                        Tile.Board.ShufflePieces.CheckNeedShuffle();

                    if (isMainPiece)
                    {
                        if (_piece.CheckSwap())
                        {
                            TileLast.Piece?.Fall();
                            OnMovesChange?.Invoke();
                        }
                        else if (TileLast.Piece?.CheckSwap() ?? false)
                        {
                            Tile.Piece?.Fall();
                            OnMovesChange?.Invoke();
                        }
                        else
                        {
                            Tile.Actions.SwapPieces(OnMovesChange, TileLast, false, true);
                        }
                    }
                });

                SetLastMoveTime();
            }, TypeActions.PieceMoving, 0, false, GameManager.Config.TIME_SWAP, false);
        }
    }
}
