using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orchard.Game
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private BoardObjectCreator _boardObjectCreator;
        [SerializeField] private TileBorder _tileBorder;
        [Space(10)]
        [SerializeField] private ComboOfPieces _comboOfPieces;
        [SerializeField] private ShufflePieces _shufflePieces;
        [SerializeField] private PieceObjectPool _poolBoardObject;
        [SerializeField] private ParticleObjectPool _particleObjectPool;
        [SerializeField] private TasksLevelInformation _tasksLevelInformation;

        public ShufflePieces ShufflePieces { get => _shufflePieces; private set => _shufflePieces = value; }
        public ParticleObjectPool ParticleObjectPool => _particleObjectPool;
        public TasksLevelInformation TasksLevelInformation => _tasksLevelInformation;
        public ComboOfPieces ComboOfPieces => _comboOfPieces;
        public RandomTypePiece RandomTypePiece => _boardObjectCreator.RandomTypePiece;

        public List<Tile> ActionTiles { get; private set; } = new List<Tile>();

        private Tile[,] _tiles;
        public Tile[,] Tiles => _tiles;

        private FallingPieces _fallingPieces;

        private bool _isNextMove;

        private void Awake()
        {
            DOTween.SetTweensCapacity(500, 150);

            _boardObjectCreator.CreateObjectsBoard(ref _tiles);

            _tileBorder.CreateBorder(_tiles);

            _fallingPieces = new FallingPieces(Tiles, ComboOfPieces);

            NextMove();
        }

        public Tile GetTile(PosXY posXY)
        {
            return GetTile(posXY.x, posXY.y);
        }

        public Tile GetTile(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Tiles.GetLength(0) || y >= Tiles.GetLength(1))
                return null;

            if (Tiles[x, y].IsEmpty)
                return null;

            return Tiles[x, y];
        }

        public Piece GetRandomPiece()
        {
            TypeBoardObject typeRandomPiece = RandomTypePiece.GetRandomTypePiece();
            Piece piece = _poolBoardObject.GetUnit(typeRandomPiece).GetComponent<Piece>();

            piece.SetType(typeRandomPiece);

            return piece;
        }

        public Piece GetPiece(TypeBoardObject typePiece)
        {
            return _poolBoardObject.GetUnit(typePiece)?.GetComponent<Piece>();
        }

        public void AddTileFall(Tile tile)
        {
            _fallingPieces.AddTileFall(tile);
        }

        public void NextMove()
        {
            if (!_isNextMove)
                StartCoroutine(FallingPiece());
        }

        private IEnumerator FallingPiece()
        {
            _isNextMove = true;
            yield return new WaitForEndOfFrame();
            _isNextMove = false;
            _fallingPieces.Fall();
        }

        public void HideBoard()
        {
            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < Tiles.GetLength(1); y++)
                {
                    Tiles[x, y].transform.DOScale(0, 0.15f).SetDelay((x + y) * 0.025f);
                }
            }
        }
    }
}
