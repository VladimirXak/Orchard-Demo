using System.Collections.Generic;
using UnityEngine;
using System;

namespace Orchard.Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Tile : MonoBehaviour, IEquatable<Tile>
    {
        [SerializeField] private TileSelect _tileSelect;
        [SerializeField] private TileActions _actions;

        public NodeTilesInbox NodeTilesInbox { get; private set; }
        public NodeTilesOutbox NodeTilesOutbox { get; private set; }

        public PosXY PosXY { get; private set; }

        public Board Board { get; private set; }

        public TileSelect TileSelect => _tileSelect;
        public TileActions Actions => _actions;

        public Piece Piece { get; private set; }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get
            {
                return _isEmpty;
            }
            set
            {
                _render.enabled = false;
                _isEmpty = value;
                _isFreePiece = false;
            }
        }

        public bool IsSpawn { get; private set; }
        public TileSpawn TileSpawn { get; private set; }

        public bool IsBusy { get; set; }

        private bool _isFreePiece;
        public bool IsFreePiece
        {
            get
            {
                if (_isFreePiece && !IsBusy)
                    return true;
                else
                    return false;
            }

            private set
            {
                _isFreePiece = value;
            }
        }

        private bool _isPlaceToPieceFree;
        public bool IsPlaceToPieceFree
        {
            get
            {
                if (_isPlaceToPieceFree && !IsBusy)
                    return true;
                else
                    return false;
            }

            private set
            {
                _isPlaceToPieceFree = value;
            }
        }

        private TileColorState _colorState;
        public TileColorState ColorState
        {
            get
            {
                return _colorState;
            }
            set
            {
                _colorState = value;
                //ChangeColor(value);
            }
        }

        private SpriteRenderer _render;

        private void Awake()
        {
            _render = GetComponent<SpriteRenderer>();

            IsPlaceToPieceFree = true;

            NodeTilesInbox = new NodeTilesInbox();
            NodeTilesOutbox = new NodeTilesOutbox();
        }

        public void Init(Board board, PosXY posXY)
        {
            Board = board;
            PosXY = posXY;
        }

        public void SetTileSpawn(TileSpawn tileSpawn)
        {
            IsSpawn = true;
            TileSpawn = tileSpawn;
        }

        public void SetPiece(Piece piece)
        {
            Piece = piece;

            ChangeStateIsFreePiece();

            if (piece == null)
            {
                IsPlaceToPieceFree = true;
                Board.AddTileFall(this);
            }
            else
            {
                IsPlaceToPieceFree = false;
            }
        }

        public void ChangeStateIsFreePiece()
        {
            if (Piece != null)
                IsFreePiece = true;
            else
                IsFreePiece = false;
        }

        private void ChangeColor(TileColorState arg)
        {
            switch (arg)
            {
                case TileColorState.RED:
                    _render.color = Color.red;
                    break;
                case TileColorState.YELLOW:
                    _render.color = Color.yellow;
                    break;
                case TileColorState.GREEN:
                    _render.color = Color.green;
                    break;
                case TileColorState.CYAN:
                    _render.color = Color.black;
                    break;
            }
        }

        public bool Equals(Tile obj)
        {
            return PosXY.Equals(obj.PosXY);
        }
    }

    public class NodeTilesInbox
    {
        public List<Tile> MainTiles { get; private set; }
        public List<Tile> ExtraTiles { get; private set; }

        public NodeTilesInbox()
        {
            MainTiles = new List<Tile>();
            ExtraTiles = new List<Tile>();
        }

        public void AddMainTile(Tile tile)
        {
            MainTiles.Add(tile);
        }

        public void AddExtraTile(Tile tile)
        {
            ExtraTiles.Add(tile);
        }
    }

    public class NodeTilesOutbox
    {
        public Tile Main { get; private set; }
        public Tile Left { get; private set; }
        public Tile Right { get; private set; }

        public void SetMain(Tile tile)
        {
            Main = tile;
        }

        public void SetLeft(Tile tile)
        {
            Left = tile;
        }

        public void SetRight(Tile tile)
        {
            Right = tile;
        }
    }
}
