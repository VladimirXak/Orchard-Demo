using Orchard.GameSpace;
using UnityEngine;

namespace Orchard.Game
{
    public class TileSpawn
    {
        private Tile _tile;
        private Vector2 _startPositionPiece;

        public TileSpawn(Tile tile, PosXY direction)
        {
            _tile = tile;

            _startPositionPiece = new Vector2(-GameManager.Config.TILE_SIZE * direction.x, GameManager.Config.TILE_SIZE * direction.y);
        }

        public void Spawn()
        {
            Piece piece = _tile.Board.GetRandomPiece();

            piece.SetParentTile(_tile);
            piece.transform.localPosition = _startPositionPiece;
            piece.gameObject.SetActive(true);

            _tile.Actions.FallPiece(piece);
        }
    }
}
