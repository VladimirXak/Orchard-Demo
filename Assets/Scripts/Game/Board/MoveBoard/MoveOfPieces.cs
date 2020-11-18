using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orchard.Game
{
    public class MoveOfPieces : MonoBehaviour
    {
        #region PatternMove

        private readonly int[][,] arrayPatternMove = new int[][,]
        {
        new int[,]
        {
            {10,10,0,10,10},
            { 9, 9,1, 9, 9 }
        },

        new int[,]
        {
            {9,9,1,9,9},
            {10,10,0,10,10}
        },

        new int[,]
        {
            {10,9},
            {10,9},
            {0, 1},
            {10,9},
            {10,9}
        },

        new int[,]
        {
            {9,10},
            {9,10},
            {1, 0},
            {9,10},
            {9,10}
        },




        new int[,]
        {
            {1,  9, 9},
            {0, 10, 10},
            {10, 9, 9},
            {10, 9, 9}
        },

        new int[,]
        {
            {1, 0, 10, 10},
            {9, 10, 9, 9},
            {9, 10, 9, 9}
        },

        new int[,]
        {
            { 9,  9,  1},
            {10, 10,  0},
            { 9,  9, 10},
            { 9,  9, 10}
        },

        new int[,]
        {
            {10, 10,  0, 1},
            { 9,  9, 10, 9},
            { 9,  9, 10, 9}
        },

        new int[,]
        {
            { 9,  9, 10},
            { 9,  9, 10},
            {10, 10,  0},
            { 9,  9,  1}
        },

        new int[,]
        {
            { 9,  9, 10, 9},
            { 9,  9, 10, 9},
            {10, 10,  0, 1}
        },

        new int[,]
        {
            {10,  9,  9},
            {10,  9,  9},
            { 0, 10, 10},
            { 1,  9,  9}
        },

        new int[,]
        {
            {9, 10,  9,  9},
            {9, 10,  9,  9},
            {1,  0, 10, 10}
        },

        new int[,]
        {
            { 9, 1, 9},
            {10, 0,10},
            { 9,10, 9},
            { 9,10, 9}
        },

        new int[,]
        {
            { 9, 9,10, 9},
            {10,10, 0, 1},
            { 9, 9,10, 9}
        },

        new int[,]
        {
            { 9,19, 9},
            { 9,10, 9},
            {19, 0,10},
            { 9, 1, 9}
        },

        new int[,]
        {
            {9,10, 9, 9},
            {1, 0,10,10},
            {9,10, 9, 9}
        },





        new int[,]
        {
            {10,0,10,10},
            { 9,1, 9, 9}
        },

        new int[,]
        {
            {10,10,0,10},
            { 9, 9,1, 9}
        },

        new int[,]
        {
            { 9, 1, 9, 9},
            {10, 0,10,10}
        },

        new int[,]
        {
            { 9,  9,1, 9},
            {10, 10,0,10}
        },

        new int[,]
        {
            {10,9},
            { 0,1},
            {10,9},
            {10,9}
        },

        new int[,]
        {
            {10,9},
            {10,9},
            { 0,1},
            {10,9}
        },

        new int[,]
        {
            {9,10},
            {1, 0},
            {9,10},
            {9,10}
        },

        new int[,]
        {
            {9,10},
            {9,10},
            {1, 0},
            {9,10}
        },




        new int[,]
        {
            {1, 0,10},
            {9,10,10}
        },

        new int[,]
        {
            { 1, 9},
            { 0,10},
            {10,10}
        },

        new int[,]
        {
            { 9, 1},
            {10, 0},
            {10,10}
        },

        new int[,]
        {
            {10, 0, 1},
            {10,10, 9}
        },

        new int[,]
        {
            {10,10, 9},
            {10, 0, 1},
        },

        new int[,]
        {
            {10,10},
            {10, 0},
            { 9, 1}
        },

        new int[,]
        {
            {10,10},
            { 0,10},
            { 1, 9}
        },

        new int[,]
        {
            {9,10,10},
            {1, 0,10}
        },





        new int[,]
        {
            {1,0,10,10}
        },

        new int[,]
        {
            {10,10,0,1}
        },

        new int[,]
        {
            {1},
            {0},
            {10},
            {10}
        },

        new int[,]
        {
            {10},
            {10},
            {0},
            {1}
        },

        new int[,]
        {
            {0,10,10},
            {1, 9, 9}
        },

        new int[,]
        {
            {10,0,10},
            {9, 1, 9}
        },

        new int[,]
        {
            {10,10,0},
            {9, 9, 1}
        },

        new int[,]
        {
            {1, 9, 9},
            {0,10,10}
        },

        new int[,]
        {
            {9, 1, 9},
            {10,0,10}
        },

        new int[,]
        {
            {9, 9, 1},
            {10,10,0}
        },

        new int[,]
        {
            {0,1},
            {10,9},
            {10,9}
        },

        new int[,]
        {
            {10,9},
            {0, 1},
            {10,9}
        },

        new int[,]
        {
            {10,9},
            {10,9},
            {0, 1}
        },

        new int[,]
        {
            {1, 0},
            {9,10},
            {9,10}
        },

        new int[,]
        {
            {9,10},
            {1, 0},
            {9,10}
        },

        new int[,]
        {
            {9,10},
            {9,10},
            {1, 0}
        },
        };

        #endregion PatternMove

        [SerializeField] private Board _board;
        private List<List<PosXY>> _moves;

        public List<List<PosXY>> FindMoves(JsonDataBoard boardInfo = null)
        {
            int[,] array;

            if (boardInfo == null)
            {
                array = GetArrayFromBoardForFindMoves();
            }
            else
            {
                array = GetArrayFromBoardInfoForFindMoves(boardInfo);
            }

            _moves = new List<List<PosXY>>();

            foreach (var pattern in arrayPatternMove)
            {
                for (int i = 1; i < 6; i++)
                {
                    FindMovePieces(ref array, pattern, i);
                }
            }

            return _moves;
        }

        private void FindMovePieces(ref int[,] array, int[,] pattern, int typePiece)
        {
            int patternWidth = pattern.GetLength(0);
            int patternHeight = pattern.GetLength(1);

            int widthBoard = array.GetLength(0) - patternHeight + 1;
            int heightBoard = array.GetLength(1) - patternWidth + 1;

            for (int wb = 0; wb < widthBoard; wb++)
            {
                for (int hb = 0; hb < heightBoard; hb++)
                {
                    for (int px = 0; px < patternWidth; px++)
                    {
                        for (int py = 0; py < patternHeight; py++)
                        {
                            if (pattern[px, py] != 9 && array[wb + py, hb + px] == 0)
                                goto Next;

                            if (pattern[px, py] == 0 && array[wb + py, hb + px] > 9)
                                goto Next;

                            if (pattern[px, py] != 0 && pattern[px, py] != 9)
                                if (pattern[px, py] * typePiece != array[wb + py, hb + px] && pattern[px, py] * typePiece / 10 != array[wb + py, hb + px])
                                    goto Next;
                        }
                    }

                    List<PosXY> tmpPosXY = new List<PosXY>();

                    for (int px = 0; px < patternWidth; px++)
                    {
                        for (int py = 0; py < patternHeight; py++)
                        {
                            if (pattern[px, py] != 0 && pattern[px, py] != 9)
                                if (pattern[px, py] * typePiece == array[wb + py, hb + px] || pattern[px, py] * typePiece / 10 == array[wb + py, hb + px])
                                {
                                    tmpPosXY.Add(new PosXY(wb + py, hb + px));
                                    array[wb + py, hb + px] = 0;
                                }
                        }
                    }

                    _moves.Add(tmpPosXY);

                    Next: continue;
                }
            }
        }

        private int[,] GetArrayFromBoardForFindMoves()
        {
            int[,] array = new int[_board.Tiles.GetLength(0), _board.Tiles.GetLength(1)];

            for (int x = 0; x < _board.Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < _board.Tiles.GetLength(1); y++)
                {
                    array[x, y] = 0;

                    if (_board.GetTile(x, y)?.Piece != null)
                    {
                        switch (_board.Tiles[x, y].Piece.Type)
                        {
                            case TypeBoardObject.PieceRed:
                                array[x, y] = 1;
                                break;
                            case TypeBoardObject.PieceYellow:
                                array[x, y] = 2;
                                break;
                            case TypeBoardObject.PieceGreen:
                                array[x, y] = 3;
                                break;
                            case TypeBoardObject.PieceBlue:
                                array[x, y] = 4;
                                break;
                            case TypeBoardObject.PieceOrange:
                                array[x, y] = 5;
                                break;
                        }
                    }
                    else if (_board.GetTile(x, y) != null)
                    {
                        array[x, y] = 8;
                    }
                }
            }

            return array;
        }

        private int[,] GetArrayFromBoardInfoForFindMoves(JsonDataBoard boardInfo)
        {
            int[,] array = new int[boardInfo.widthBoard, boardInfo.heightBoard];

            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    array[x, y] = 0;

                    if (!boardInfo.tilesDetail[x, y].isEnable)
                        continue;

                    switch (boardInfo.tilesDetail[x, y].typePiece)
                    {
                        case TypeBoardObject.PieceRed:
                            array[x, y] = 1;
                            break;
                        case TypeBoardObject.PieceYellow:
                            array[x, y] = 2;
                            break;
                        case TypeBoardObject.PieceGreen:
                            array[x, y] = 3;
                            break;
                        case TypeBoardObject.PieceBlue:
                            array[x, y] = 4;
                            break;
                        case TypeBoardObject.PieceOrange:
                            array[x, y] = 5;
                            break;
                        default:
                            array[x, y] = 8;
                            break;
                    }
                }
            }

            return array;
        }
    }
}