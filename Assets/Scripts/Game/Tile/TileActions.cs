using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Orchard.Game
{
    [RequireComponent(typeof(Tile))]
    [RequireComponent(typeof(TileActivities))]
    public class TileActions : MonoBehaviour
    {
        private Tile _tile;

        public TileActivities TileActivities { get; private set; }

        public bool IsSwap { get; set; }

        private void Awake()
        {
            _tile = GetComponent<Tile>();
            TileActivities = GetComponent<TileActivities>();
        }

        public void NewPiece(Piece piece, TypeBoardObject type)
        {
            piece.SetParentTile(_tile);
            piece.SetType(type);

            piece.transform.localPosition = Vector3.zero;
            piece.gameObject.SetActive(true);
        }

        public bool TapActivation()
        {
            return _tile.Piece?.TapActivation() ?? false;
        }

        public void HitMatch()
        {
            if (_tile.Piece != null)
            {
                TypeBoardObject typeHit = _tile.Piece.Type;

                _tile.Piece.HitPiece();
                HitNear(typeHit);
            }
        }

        public void HitNear(TypeBoardObject typeHit)
        {
            List<PosXY> listPositionNear = new List<PosXY>() { new PosXY(1, 0), new PosXY(-1, 0), new PosXY(0, 1), new PosXY(0, -1) };

            foreach (var posXY in listPositionNear)
            {
                PosXY dPos = posXY + _tile.PosXY;

                _tile.Board.GetTile(dPos.x, dPos.y)?.Piece?.HitNear();
            }
        }

        public void SwapPieces(Action OnMovesChange, Tile tileTwo, bool isFinger = false, bool isBack = false)
        {
            Piece pieceOne = _tile.Piece;
            Piece pieceTwo = tileTwo.Piece;

            TypeSwap typeSwap = TypeSwap.Pieces;

            if (pieceTwo != null)
            {
                pieceTwo.SetParentTile(_tile);
                pieceTwo.SwappingPieces.Swap(OnMovesChange, typeSwap, false, isBack);
            }
            else
            {
                _tile.SetPiece(null);
                _tile.Actions.IsSwap = true;
            }


            if (pieceOne != null)
            {
                pieceOne.SetParentTile(tileTwo);
                pieceOne.SwappingPieces.Swap(OnMovesChange, typeSwap, isFinger, isBack);
            }
            else
            {
                tileTwo.SetPiece(null);
                tileTwo.Actions.IsSwap = true;
            }
        }

        public void ActivateBooster(float delay)
        {
            TileActivities.NewAction(() =>
            {
                _tile.SetPiece(null);
            },
            TypeActions.BoosterDelay, delay);
        }

        public void FallPiece(Piece newPiece)
        {
            newPiece.SetParentTile(_tile);
            _tile.Piece.FallingToCenter();
        }

        public void ShufflePiece(TypeBoardObject newTypePiece)
        {
            _tile.Piece.transform.DOScale(Vector2.zero, 0.2f).OnComplete(delegate
            {
                _tile.Piece.ShufflePiece(newTypePiece);

                _tile.Piece.transform.DOScale(Vector2.one, 0.2f);
            });
        }
    }
}
