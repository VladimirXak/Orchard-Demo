using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orchard.Game
{
    public class ComboOfPieces : MonoBehaviour
    {
        #region PatternMatches

        private readonly int[][,] arrayPatternMatches = new int[][,]
        {
        new int[,]
        {
            {1,1,1,1,1},
            {0,0,1,0,0},
            {0,0,1,0,0}
        },

        new int[,]
        {
            {0,0,1},
            {0,0,1},
            {1,1,1},
            {0,0,1},
            {0,0,1}
        },

        new int[,]
        {
            {0,0,1,0,0},
            {0,0,1,0,0},
            {1,1,1,1,1}
        },

        new int[,]
        {
            {1,0,0},
            {1,0,0},
            {1,1,1},
            {1,0,0},
            {1,0,0}
        },

        new int[,]
        {
            {1,1,1,1,1}
        },

        new int[,]
        {
            {1},
            {1},
            {1},
            {1},
            {1}
        },





        new int[,]
        {
            {1,1,1,1},
            {0,1,0,0},
            {0,1,0,0}
        },

        new int[,]
        {
            {1,0,0},
            {1,1,1},
            {1,0,0},
            {1,0,0}
        },

        new int[,]
        {
            {1,1,1,1},
            {0,0,1,0},
            {0,0,1,0}
        },

        new int[,]
        {
            {0,0,1},
            {1,1,1},
            {0,0,1},
            {0,0,1}
        },

        new int[,]
        {
            {0,0,1,0},
            {0,0,1,0},
            {1,1,1,1},
        },

        new int[,]
        {
            {0,0,1},
            {0,0,1},
            {1,1,1},
            {0,0,1}
        },

        new int[,]
        {
            {1,0,0},
            {1,0,0},
            {1,1,1},
            {1,0,0},
        },

        new int[,]
        {
            {0,1,0,0},
            {0,1,0,0},
            {1,1,1,1},
        },

        new int[,]
        {
            {0,1,0},
            {0,1,0},
            {1,1,1}
        },

        new int[,]
        {
            {1,1,1},
            {0,1,0},
            {0,1,0}
        },

        new int[,]
        {
            {1,0,0},
            {1,1,1},
            {1,0,0}
        },

        new int[,]
        {
            {0,0,1},
            {1,1,1},
            {0,0,1}
        },

        new int[,]
        {
            {1,1,1},
            {1,0,0},
            {1,0,0}
        },

        new int[,]
        {
            {1,1,1},
            {0,0,1},
            {0,0,1}
        },

        new int[,]
        {
            {0,0,1},
            {0,0,1},
            {1,1,1}
        },

        new int[,]
        {
            {1,0,0},
            {1,0,0},
            {1,1,1}
        },




        new int[,]
        {
            {1,1,1,1}
        },

        new int[,]
        {
            {1},
            {1},
            {1},
            {1}
        },





        new int[,]
        {
            {0,1,1},
            {1,1,1}
        },

        new int[,]
        {
            {1,1,1},
            {0,1,1}
        },

        new int[,]
        {
            {1,1,1},
            {1,1,0}
        },

        new int[,]
        {
            {1,1,0},
            {1,1,1}
        },

        new int[,]
        {
            {0,1},
            {1,1},
            {1,1}
        },

        new int[,]
        {
            {1,0},
            {1,1},
            {1,1}
        },

        new int[,]
        {
            {1,1},
            {1,1},
            {1,0}
        },

        new int[,]
        {
            {1,1},
            {1,1},
            {0,1}
        },

        new int[,]
        {
            {1,1},
            {1,1}
        },





        new int[,]
        {
            {1,1,1}
        },

        new int[,]
        {
            {1},
            {1},
            {1}
        }
    };

        #endregion PatternMatches

        [SerializeField] private Board _board;
        [SerializeField] private ComboActivator _comboActivator;

        public ComboActivator ComboActivator { get => _comboActivator; }

        private List<List<PosXY>> _combos;

        public List<List<PosXY>> FindCombos(JsonDataBoard boardInfo = null)
        {
            int[,] array;

            if (boardInfo == null)
            {
                array = GetArrayFromBoard();
            }
            else
            {
                array = GetArrayFromBoardInfo(boardInfo);
            }

            _combos = new List<List<PosXY>>();

            for (int i = 1; i < 6; i++)
            {
                foreach (var pattern in arrayPatternMatches)
                {
                    FindComboMatches(ref array, pattern, i);
                }
            }

            return _combos;
        }

        private void FindComboMatches(ref int[,] array, int[,] pattern, int typePiece)
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
                            if (pattern[px, py] != 0)
                            {
                                if (pattern[px, py] * typePiece != array[wb + py, hb + px])
                                    goto Next;
                            }
                        }
                    }

                    List<PosXY> tmpPosXY = new List<PosXY>();

                    for (int py = 0; py < patternHeight; py++)
                    {
                        for (int px = 0; px < patternWidth; px++)
                        {
                            if (pattern[px, py] != 0)
                                if (pattern[px, py] * typePiece == array[wb + py, hb + px])
                                {
                                    tmpPosXY.Add(new PosXY(wb + py, hb + px));
                                    array[wb + py, hb + px] = 0;
                                }
                        }
                    }

                    _combos.Add(tmpPosXY);

                    Next: continue;
                }
            }
        }

        private int[,] GetArrayFromBoard()
        {
            int[,] array = new int[_board.Tiles.GetLength(0), _board.Tiles.GetLength(1)];

            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    array[x, y] = 0;

                    if (_board.GetTile(x, y)?.Piece == null || _board.Tiles[x, y].IsBusy)
                        continue;

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
                        default:
                            array[x, y] = 10;
                            break;
                    }
                }
            }

            return array;
        }

        private int[,] GetArrayFromBoardInfo(JsonDataBoard boardInfo)
        {
            int[,] array = new int[boardInfo.widthBoard, boardInfo.heightBoard];

            for (int x = 0; x < boardInfo.widthBoard; x++)
            {
                for (int y = 0; y < boardInfo.heightBoard; y++)
                {
                    array[x, y] = 0;

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
                            array[x, y] = 10;
                            break;
                    }
                }
            }

            return array;
        }
    }
}
