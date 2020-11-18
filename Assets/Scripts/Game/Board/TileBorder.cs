using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Orchard.Game
{
    public class TileBorder : MonoBehaviour
    {
        #region Border

        // Для формирования бордюра
        // 0 - пуста
        // 1 - заполнена
        // 7 - не используется для сравнения
        // 8 - любая (пустая или заполненная)
        private int[][] arrBorder = new int[][]
        {
        // 0
        new int[9]
        {
        0,0,0,
        0,0,0,
        0,0,0
        },
        // 1
        new int[9]
        {
        0,0,7,
        0,1,7,
        7,7,7
        },
        // 2
        new int[9]
        {
        7,0,0,
        7,1,0,
        7,7,7
        },
        // 3
        new int[9]
        {
        7,7,7,
        7,1,0,
        7,0,0
        },
        // 4
        new int[9]
        {
        7,7,7,
        0,1,7,
        0,0,7
        },
        // 5
        new int[9]
        {
        7,0,0,
        7,1,1,
        7,7,7
        },
        // 6
        new int[9]
        {
        0,0,7,
        1,1,7,
        7,7,7
        },
        // 7
        new int[9]
        {
        7,7,7,
        7,1,0,
        7,1,0
        },
        // 8
        new int[9]
        {
        7,1,0,
        7,1,0,
        7,7,7
        },
        // 9
        new int[9]
        {
        7,7,7,
        1,1,7,
        0,0,7
        },
        // 10
        new int[9]
        {
        7,7,7,
        7,1,1,
        7,0,0
        },
        // 11
        new int[9]
        {
        0,1,7,
        0,1,7,
        7,7,7
        },
        // 12
        new int[9]
        {
        7,7,7,
        0,1,7,
        0,1,7
        },
        // 13
        new int[9]
        {
        8,1,7,
        1,0,7,
        7,7,7
        },
        // 14
        new int[9]
        {
        7,1,8,
        7,0,1,
        7,7,7
        },
        // 15
        new int[9]
        {
        7,7,7,
        7,0,1,
        7,1,8
        },
        // 16
        new int[9]
        {
        7,7,7,
        1,0,7,
        8,1,7
        },
        };

        #endregion

        public void CreateBorder(Tile[,] tiles)
        {
            List<PosXY> dPosXY = new List<PosXY>() { new PosXY(-1, -1), new PosXY(0, -1), new PosXY(1, -1), new PosXY(-1, 0), new PosXY(0, 0), new PosXY(1, 0), new PosXY(-1, 1), new PosXY(0, 1), new PosXY(1, 1) };

            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    Tile tile = tiles[x, y];
                    int[] arr = new int[9];

                    for (int i = 0; i < dPosXY.Count; i++)
                    {
                        PosXY dp = dPosXY[i] + tile.PosXY;

                        Tile tempTile = tile.Board.GetTile(dp);

                        if (!tempTile?.IsEmpty ?? false)
                            arr[i] = 1;
                        else
                            arr[i] = 0;
                    }

                    SetupBorder(tile.transform, arr);
                }
            }
        }

        public void SetupBorder(Transform tileTransform, int[] arr)
        {
            List<int> list = GetListBorders(arr);

            if (list.Count > 0)
            {
                foreach (int ii in list)
                {
                    if (ii <= 28)
                    {
                        GameObject go = new GameObject("Border: " + ii);
                        SpriteRenderer spr = go.AddComponent<SpriteRenderer>();
                        spr.sprite = LoadBorderImg("border-Background", ii);
                        spr.sortingLayerName = "Board";
                        spr.sortingOrder = 0;
                        spr.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;

                        go.transform.SetParent(tileTransform);
                        go.transform.localPosition = Vector3.zero;
                        go.transform.localScale = Vector3.one;

                        go.isStatic = true;
                    }
                }
            }
        }

        public List<int> GetListBorders(int[] arr)
        {
            List<int> list = new List<int>();

            for (int i = 1; i < arrBorder.Length; i++)
            {
                int both = 0;
                for (int k = 0; k < 9; k++)
                {
                    if (arrBorder[i][k] == arr[k] || arrBorder[i][k] == 8)
                        both++;
                }

                if (both == 4)
                {
                    list.Add(i);
                }
            }

            return list;
        }

        public Sprite LoadBorderImg(string borderName, int num)
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>("MatchImgs/" + borderName);
            Sprite spr = sprites.Single(s => (s.name == num.ToString()));

            return spr;
        }
    }
}
