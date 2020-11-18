using System.Collections.Generic;
using UnityEngine;

namespace Orchard.Game
{
    public class BoardObjectCreator : MonoBehaviour
    {
        [SerializeField] private BoardInfoCreator _boardInfoCreator;
        [SerializeField] private TileCreator _tileCreator;
        [Space(10)]
        [SerializeField] private PieceObjectPool _pieceObjectPool;

        private RandomTypePiece _randomTypePiece;
        public RandomTypePiece RandomTypePiece { get => _randomTypePiece; private set => _randomTypePiece = value; }

        public void CreateObjectsBoard(ref Tile[,] tiles)
        {
            JsonDataBoard boardInfo = _boardInfoCreator.CreateBoardInfo(ref _randomTypePiece);

            tiles = _tileCreator.Init(boardInfo.widthBoard, boardInfo.heightBoard);

            for (int x = 0; x < boardInfo.widthBoard; x++)
            {
                for (int y = 0; y < boardInfo.heightBoard; y++)
                {
                    JsonTileDetail tileDetail = boardInfo.tilesDetail[x, y];

                    if (!tileDetail.isEnable)
                    {
                        tiles[x, y].IsEmpty = true;
                        continue;
                    }

                    if (tileDetail.isSpawnerPieces)
                    {
                        tiles[x, y].SetTileSpawn(new TileSpawn(tiles[x, y], tileDetail.direction));
                    }

                    if (tileDetail.typePiece != TypeBoardObject.NULL)
                    {
                        GameObject newPiece = _pieceObjectPool.GetUnit(tileDetail.typePiece);
                        tiles[x, y].Actions.NewPiece(newPiece.GetComponent<Piece>(), tileDetail.typePiece);
                    }
                }
            }

            for (int x = 0; x < boardInfo.widthBoard; x++)
            {
                for (int y = 0; y < boardInfo.heightBoard; y++)
                {
                    ConfigurateNodeInbox(tiles, boardInfo.tilesDetail, tiles[x, y]);
                    ConfigurateNodeOutbox(tiles, tiles[x, y], boardInfo.tilesDetail[x, y].direction);
                }
            }
        }

        private void ConfigurateNodeInbox(Tile[,] tiles, JsonTileDetail[,] tilesDetail, Tile tile)
        {
            if (tile.IsEmpty)
                return;

            List<PosXY> listNearPosXY = new List<PosXY>() { new PosXY(-1, 0), new PosXY(-1, -1), new PosXY(0, -1), new PosXY(1, -1), new PosXY(1, 0), new PosXY(1, 1), new PosXY(0, 1), new PosXY(-1, 1) };

            foreach (var nearPosXY in listNearPosXY)
            {
                Tile tempTile = GetTileNode(tiles, tile.PosXY + nearPosXY);

                if (tempTile == null)
                    continue;

                PosXY dDirection = nearPosXY + tilesDetail[tempTile.PosXY.x, tempTile.PosXY.y].direction;

                int alphaSumDirection = Mathf.Abs(dDirection.x) + Mathf.Abs(dDirection.y);

                if (alphaSumDirection == 0)
                {
                    tile.NodeTilesInbox.AddMainTile(tempTile);
                }
                else if (alphaSumDirection == 1)
                {
                    tile.NodeTilesInbox.AddExtraTile(tempTile);
                }
            }
        }

        private void ConfigurateNodeOutbox(Tile[,] tiles, Tile tile, PosXY direction)
        {
            PosXY posLeft;
            PosXY posRight;

            if (direction.Equals(new PosXY(0, 1)))
            {
                posLeft = tile.PosXY + new PosXY(-1, 1);
                posRight = tile.PosXY + new PosXY(1, 1);
            }
            else if (direction.Equals(new PosXY(0, -1)))
            {
                posLeft = tile.PosXY + new PosXY(1, -1);
                posRight = tile.PosXY + new PosXY(-1, -1);
            }
            else if (direction.Equals(new PosXY(1, 0)))
            {
                posLeft = tile.PosXY + new PosXY(1, 1);
                posRight = tile.PosXY + new PosXY(1, -1);
            }
            else
            {
                posLeft = tile.PosXY + new PosXY(-1, -1);
                posRight = tile.PosXY + new PosXY(-1, 1);
            }

            Tile leftTile = GetTileNode(tiles, posLeft);
            Tile rightTile = GetTileNode(tiles, posRight);
            Tile mainTile = GetTileNode(tiles, tile.PosXY + direction);

            tile.NodeTilesOutbox.SetMain(mainTile);
            tile.NodeTilesOutbox.SetLeft(leftTile);
            tile.NodeTilesOutbox.SetRight(rightTile);
        }

        private Tile GetTileNode(Tile[,] tiles, PosXY posXY)
        {
            if (posXY.x >= 0 && posXY.x < tiles.GetLength(0) && posXY.y >= 0 && posXY.y < tiles.GetLength(1))
            {
                Tile tile = tiles[posXY.x, posXY.y];

                return tile.IsEmpty ? null : tile;
            }

            return null;
        }
    }
}
