using Orchard.GameSpace;
using UnityEngine;

namespace Orchard.Game
{
    public class TileCreator : MonoBehaviour
    {
        [SerializeField] private Board _board;
        [SerializeField] private Tile _prefTile;

        public Tile[,] Init(int width, int height)
        {
            Tile[,] tiles = new Tile[width, height];

            float startPositionX = -FindStartPosition(width);
            float positionY = FindStartPosition(height);

            float sizeTile = GameManager.Config.TILE_SIZE;

            for (int y = 0; y < height; y++)
            {
                float positionX = startPositionX;

                for (int x = 0; x < width; x++)
                {
                    Tile tile = Instantiate(_prefTile, transform);
                    tile.name = $"Tile [{x}, {y}]";
                    tile.gameObject.isStatic = true;
                    tile.transform.SetParent(transform);
                    tile.Init(_board, new PosXY(x, y));
                    tile.transform.localPosition = new Vector3(positionX, positionY);

                    tiles[x, y] = tile;

                    positionX += sizeTile;
                }

                positionY -= sizeTile;
            }

            return tiles;
        }

        private float FindStartPosition(int lenght)
        {
            float tileSize = GameManager.Config.TILE_SIZE;

            int a = lenght / 2;

            return (a * tileSize) + ((lenght % 2 == 0) ? -(tileSize / 2) : 0);
        }
    }
}
