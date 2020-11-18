using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using Orchard.GameSpace;

namespace Orchard.Game
{
    public class BoardTapController : MonoBehaviour
    {
        [SerializeField] private Board _board;

        public event Action OnMovesChange;

        private Tile _tileMain;
        private Tile _tileOne;
        private Tile _tileTwo;

        private Vector2 _startDragPos;

        private DragState _dragState;
        private float _deltaDrag;

        public bool IsCanTap { get; set; } = true;

        private void Awake()
        {
            if (Screen.width < Screen.height)
                _deltaDrag = GameManager.Config.DELTA_DRAG * (float)Screen.height / 1920f;
            else
                _deltaDrag = GameManager.Config.DELTA_DRAG * (float)Screen.width / 1080f;
        }

        private void Update()
        {
            if (!IsCanTap || EventSystem.current.IsPointerOverGameObject()) return;

            if (Input.GetMouseButtonDown(0))
            {
                TapDown();
            }

            if (Input.GetMouseButtonUp(0) && _dragState == DragState.DRAGGING_PIECE)
            {
                TapUp();
            }

            if (_dragState == DragState.DRAGGING_PIECE)
            {
                TapDrag();
            }
        }

        private void TapDown()
        {
            if (_tileOne == null)
            {
                Tile tile = TapFindTile();

                if (tile?.Piece == null)
                    return;

                _tileOne = tile;
                _tileMain = tile;

                _startDragPos = Input.mousePosition;

                _tileOne.TileSelect.StartSelect();

                _dragState = DragState.DRAGGING_PIECE;
            }
            else
            {
                Tile tile = TapFindTile();

                if (tile == null)
                    return;

                if (tile == _tileOne)
                {
                    _tileTwo = tile;
                    _tileMain = tile;
                    _startDragPos = Input.mousePosition;
                    _dragState = DragState.DRAGGING_PIECE;
                    return;
                }

                _startDragPos = Input.mousePosition;

                if (IsTilesBeside(_tileOne.PosXY, tile.PosXY))
                {
                    if (tile.IsEmpty)
                        return;

                    _tileTwo = tile;
                    _tileMain = tile;

                    _tileOne.TileSelect.StopSelect();
                    _dragState = DragState.DRAGGING_PIECE;
                }
                else
                {
                    if (tile.IsEmpty)
                        return;

                    _tileOne.TileSelect.StopSelect();
                    _tileOne = null;
                    _tileMain = null;

                    if (!tile.IsFreePiece)
                        return;

                    _tileOne = tile;
                    _tileMain = tile;

                    _tileOne.TileSelect.StartSelect();

                    _dragState = DragState.DRAGGING_PIECE;
                }
            }
        }

        private void TapUp()
        {
            if (_tileTwo != null)
            {
                if (_tileOne != _tileTwo)
                {
                    SwapTwoPieces();
                    _tileOne = null;
                }
                else
                {
                    if (_tileOne.Actions.TapActivation())
                        OnMovesChange?.Invoke();

                    _tileMain.TileSelect.StopSelect();
                    _tileOne = null;
                }

                _tileTwo = null;
                _tileMain = null;
            }

            _dragState = DragState.WAITING;
        }

        private void TapDrag()
        {
            Tile tile = GetDragTile();

            if (tile == null)
                return;

            _tileMain.TileSelect.StopSelect();
            _tileOne = _tileMain;
            _tileTwo = tile;

            SwapTwoPieces();

            _tileOne = null;
            _tileTwo = null;
            _tileMain = null;

            _dragState = DragState.WAITING;
        }

        private Tile GetDragTile()
        {
            Vector2 dragPosition = Input.mousePosition;

            if (Mathf.Abs(_startDragPos.x - dragPosition.x) > _deltaDrag || Mathf.Abs(_startDragPos.y - dragPosition.y) > _deltaDrag)
            {
                PosXY newPosXY = Direction(dragPosition - _startDragPos) + _tileMain.PosXY;

                Tile dragTile = _board.GetTile(newPosXY.x, newPosXY.y);

                if (dragTile != null)
                    return dragTile;
            }

            return null;
        }

        private PosXY Direction(Vector2 deltaPos)
        {
            if (Mathf.Abs(deltaPos.x) > Mathf.Abs(deltaPos.y))
            {
                if (deltaPos.x > 0)
                    return new PosXY(1, 0);
                else
                    return new PosXY(-1, 0);
            }
            else
            {
                if (deltaPos.y > 0)
                    return new PosXY(0, -1);
                else
                    return new PosXY(0, 1);
            }
        }

        private Tile TapFindTile()
        {
            Collider2D hit = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            return hit?.GetComponent<Tile>();
        }

        private bool IsTilesBeside(PosXY posOne, PosXY posTwo)
        {
            List<PosXY> listDposXY = new List<PosXY>() { new PosXY(-1, 0), new PosXY(1, 0), new PosXY(0, 1), new PosXY(0, -1) };

            foreach (PosXY posXY in listDposXY)
            {
                PosXY dPos = posXY + posOne;

                if (dPos.Equals(posTwo))
                    return true;
            }

            return false;
        }

        private void SwapTwoPieces()
        {
            if (!_tileOne.IsBusy && !_tileTwo.IsBusy)
                _tileOne.Actions.SwapPieces(OnMovesChange, _tileTwo, true);

            _dragState = DragState.WAITING;
        }
    }
}
