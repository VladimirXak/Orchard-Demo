using Orchard.GameSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orchard.Game
{
    public class ShufflePieces : MonoBehaviour
    {
        [SerializeField] private MoveOfPieces _moveOfPieces;

        [SerializeField] private Board _board;
        [SerializeField] private ComboOfPieces _comboOfPieces;

        private Coroutine _coroutineShuffle;
        private Coroutine _coroutineHint;

        private List<PosXY> _listPosXYMove;

        private bool _isShowHint = true;

        public void StopHint()
        {
            if (_coroutineHint != null)
                StopCoroutine(_coroutineHint);

            _isShowHint = false;
        }

        public void CheckNeedShuffle()
        {
            _listPosXYMove = null;

            var moves = _moveOfPieces.FindMoves();

            if (moves.Count != 0)
                _listPosXYMove = moves[0];

            if (_listPosXYMove == null)
            {
                if (_coroutineShuffle != null)
                    StopCoroutine(_coroutineShuffle);

                _coroutineShuffle = StartCoroutine(CoroutineShufflePieces());
            }
            else
            {
                if (!_isShowHint)
                    return;

                if (_coroutineHint != null)
                    StopCoroutine(_coroutineHint);

                _coroutineHint = StartCoroutine(CoroutineShowHint());
            }
        }

        private IEnumerator CoroutineShufflePieces()
        {
            yield return null;

            float time = Time.time;

            while (Time.time < time + GameManager.Config.SHUFFLE_DELAY)
            {
                yield return null;

                if (_board.ActionTiles.Count > 0)
                    yield break;
            }

            JsonDataBoard boardInfo = new JsonDataBoard()
            {
                widthBoard = _board.Tiles.GetLength(0),
                heightBoard = _board.Tiles.GetLength(1),
            };

            boardInfo.tilesDetail = new JsonTileDetail[_board.Tiles.GetLength(0), _board.Tiles.GetLength(1)];

            List<Tile> tilesReshuffle = new List<Tile>();

            IBoardObjectChecking generalPieces = new PieceGeneralChecking();

            for (int x = 0; x < _board.Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < _board.Tiles.GetLength(1); y++)
                {
                    boardInfo.tilesDetail[x, y] = new JsonTileDetail();
                    Tile tempTile = _board.GetTile(x, y);

                    if (tempTile != null)
                    {
                        if ((tempTile.IsFreePiece) && generalPieces.Check(tempTile.Piece.Type))
                        {
                            tilesReshuffle.Add(_board.Tiles[x, y]);
                            boardInfo.tilesDetail[x, y].baseTypePiece = TypeBoardObject.PieceRnd;
                        }
                    }
                    else
                    {
                        boardInfo.tilesDetail[x, y].isEnable = false;
                    }
                }
            }

            bool isHaveMove = false;

            for (int i = 0; i < 50; i++)
            {
                ChangeRandomPieces(_board.RandomTypePiece, boardInfo);

                if (IsRidOfCompos(_board.RandomTypePiece, boardInfo))
                {
                    List<List<PosXY>> moves = _moveOfPieces.FindMoves(boardInfo);

                    if (moves.Count != 0)
                    {
                        isHaveMove = true;
                        break;
                    }
                }
            }

            if (!isHaveMove)
            {
                Debug.Log("No find moves");
                yield break;
            }

            foreach (Tile tile in tilesReshuffle)
            {
                tile.Actions.ShufflePiece(boardInfo.tilesDetail[tile.PosXY.x, tile.PosXY.y].typePiece);
            }

            yield return null;

            if (_coroutineHint != null)
                StopCoroutine(_coroutineHint);

            _coroutineHint = StartCoroutine(CoroutineShowHint());
        }

        private void ChangeRandomPieces(RandomTypePiece randomTypePiece, JsonDataBoard boardInfo)
        {
            for (int x = 0; x < boardInfo.widthBoard; x++)
            {
                for (int y = 0; y < boardInfo.heightBoard; y++)
                {
                    if (boardInfo.tilesDetail[x, y].baseTypePiece == TypeBoardObject.PieceRnd)
                    {
                        boardInfo.tilesDetail[x, y].typePiece = randomTypePiece.GetRandomTypePieceBoard();
                    }
                }
            }
        }

        private bool IsRidOfCompos(RandomTypePiece randomTypePiece, JsonDataBoard boardInfo)
        {
            for (int i = 0; i < 30; i++)
            {
                List<List<PosXY>> combos = _comboOfPieces.FindCombos(boardInfo);

                if (combos.Count > 0)
                {
                    foreach (var combo in combos)
                    {
                        foreach (PosXY posXY in combo)
                        {
                            boardInfo.tilesDetail[posXY.x, posXY.y].typePiece = randomTypePiece.GetRandomTypePieceBoard();
                        }
                    }
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        private IEnumerator CoroutineShowHint()
        {
            yield return null;

            float t = 0;

            while (t < GameManager.Config.HINT_DELAY)
            {
                t += Time.deltaTime;
                yield return null;

                if (_board.ActionTiles.Count > 0)
                    yield break;
            }

            var moves = _moveOfPieces.FindMoves();

            if (moves.Count == 0)
                yield break;
            else
                _listPosXYMove = moves[0];

            if (_listPosXYMove == null)
                yield break;

            while (_board.ActionTiles.Count == 0 && _listPosXYMove != null)
            {
                foreach (PosXY posXY in _listPosXYMove)
                {
                    if (_board.Tiles[posXY.x, posXY.y].Piece != null)
                    {
                        _board.Tiles[posXY.x, posXY.y].Piece.PieceAnimations.AnimHint();
                    }
                }

                t = 0;

                while (t < GameManager.Config.DELAY_BETWEEN_HINTS)
                {
                    t += Time.deltaTime;

                    yield return null;

                    if (_board.ActionTiles.Count > 0 || _listPosXYMove == null)
                        yield break;
                }
            }
        }
    }
}
