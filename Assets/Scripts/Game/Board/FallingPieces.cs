using System.Collections.Generic;

namespace Orchard.Game
{
    public class FallingPieces
    {
        private Tile[,] _tiles;
        private TileColorState[,] _tileCS;

        private ComboOfPieces _comboOfPieces;

        private int _width;
        private int _height;

        private Queue<Tile> _queueTileFall;
        private Dictionary<PosXY, Tile> _dictionaryTileFall;

        public FallingPieces(Tile[,] tiles, ComboOfPieces comboOfPieces)
        {
            _queueTileFall = new Queue<Tile>();
            _dictionaryTileFall = new Dictionary<PosXY, Tile>();

            this._tiles = tiles;
            this._comboOfPieces = comboOfPieces;

            _width = tiles.GetLength(0);
            _height = tiles.GetLength(1);

            _tileCS = new TileColorState[_width, _height];
        }

        public void AddTileFall(Tile tile)
        {
            if (tile.IsFreePiece || tile.IsPlaceToPieceFree)
                _queueTileFall.Enqueue(tile);
        }

        public void Fall()
        {
            ChangeColorStatusTiles();

            if (FallPieces())
                _comboOfPieces.ComboActivator.ActivateCombo(_comboOfPieces.FindCombos());
        }

        private void ChangeColorStatusTiles()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    Tile tempTile = _tiles[x, y];

                    if (tempTile.IsEmpty)
                    {
                        _tileCS[x, y] = TileColorState.RED;
                        _tiles[x, y].ColorState = TileColorState.RED;
                        continue;
                    }
                    else if (tempTile.IsBusy || tempTile.Actions.IsSwap)
                    {
                        _tileCS[x, y] = TileColorState.CYAN;
                    }
                    else if (tempTile.IsSpawn)
                    {
                        _tileCS[x, y] = TileColorState.GREEN;
                    }
                    else
                    {
                        var nodeInbox = tempTile.NodeTilesInbox;

                        if (CheckTiles(nodeInbox.MainTiles, (tile) => { return tile == null; }) && CheckTiles(nodeInbox.ExtraTiles, (tile) => { return tile == null; }))
                        {
                            _tileCS[x, y] = TileColorState.RED;
                        }
                        else if (!CheckTiles(nodeInbox.MainTiles, (tile) => { return !tile.IsFreePiece; }))
                        {
                            _tileCS[x, y] = TileColorState.GREEN;
                        }
                        else if (CheckTiles(nodeInbox.MainTiles, (tile) => { return tile.ColorState == TileColorState.RED; })
                            && !CheckTiles(nodeInbox.ExtraTiles, (tile) => { return !tile.IsFreePiece; }))
                        {
                            _tileCS[x, y] = TileColorState.GREEN;
                        }
                        else if (CheckTiles(nodeInbox.MainTiles, (tile) => { return tile.ColorState == TileColorState.RED; })
                            && CheckTiles(nodeInbox.ExtraTiles, (tile) => { return tile.ColorState == TileColorState.RED; }))
                        {
                            _tileCS[x, y] = TileColorState.RED;
                        }
                        else
                        {
                            _tileCS[x, y] = TileColorState.YELLOW;
                        }
                    }

                    tempTile.ColorState = _tileCS[x, y];
                }
            }
        }

        private bool FallPieces()
        {
            bool isFindCombo = true;

            while (_queueTileFall.Count != 0)
                RightFall(ref isFindCombo, _queueTileFall.Dequeue());

            bool isRepeat = false;

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    Tile tempTile = _tiles[x, y];

                    if (tempTile.IsPlaceToPieceFree && tempTile.ColorState == TileColorState.GREEN)
                    {
                        var nodeInbox = tempTile.NodeTilesInbox;

                        if (CheckTiles(nodeInbox.MainTiles, (tile) => { return tile.ColorState == TileColorState.RED; }))
                        {
                            List<Tile> listTiles = new List<Tile>();

                            foreach (var tile in nodeInbox.ExtraTiles)
                            {
                                if (tile.IsFreePiece && tile.ColorState != TileColorState.RED)
                                    listTiles.Add(tile);
                            }

                            if (listTiles.Count != 0)
                            {
                                Tile tileFall = listTiles.GetRandom();

                                tempTile.Actions.FallPiece(tileFall.Piece);
                                tileFall.SetPiece(null);
                                tempTile.ColorState = TileColorState.CYAN;
                                isRepeat = true;
                            }
                        }
                        else
                        {
                            AddTileFall(tempTile);
                        }
                    }
                }
            }

            if (isRepeat)
                FallPieces();

            return isFindCombo;
        }

        private void RightFall(ref bool isFindCombo, Tile tile)
        {
            if (_dictionaryTileFall.ContainsKey(tile.PosXY))
                _dictionaryTileFall.Remove(tile.PosXY);

            if (tile.IsPlaceToPieceFree)
            {
                if (tile.ColorState == TileColorState.GREEN)
                {
                    var nodeInbox = tile.NodeTilesInbox;

                    List<Tile> listTiles = new List<Tile>();

                    foreach (var tempTile in nodeInbox.MainTiles)
                    {
                        if (tempTile.IsFreePiece)
                            listTiles.Add(tempTile);
                    }

                    if (listTiles.Count != 0)
                    {
                        var tileFall = listTiles.GetRandom();

                        tile.Actions.FallPiece(tileFall.Piece);
                        tileFall.SetPiece(null);
                        tile.ColorState = TileColorState.CYAN;
                        isFindCombo = false;
                        return;
                    }

                    if (tile.IsSpawn)
                    {
                        tile.TileSpawn.Spawn();
                    }
                }
            }
            else if (tile.IsFreePiece)
            {
                var nodeOutbox = tile.NodeTilesOutbox;

                if ((nodeOutbox.Main?.IsPlaceToPieceFree ?? false) && nodeOutbox.Main.ColorState == TileColorState.GREEN)
                {
                    nodeOutbox.Main.Actions.FallPiece(tile.Piece);
                    tile.SetPiece(null);
                    nodeOutbox.Main.ColorState = TileColorState.CYAN;
                    isFindCombo = false;
                }
            }
        }

        private bool CheckTiles(List<Tile> listTiles, System.Func<Tile, bool> action)
        {
            foreach (Tile tile in listTiles)
            {
                if (!action.Invoke(tile))
                    return false;
            }

            return true;
        }
    }
}
