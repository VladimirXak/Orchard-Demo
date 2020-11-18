using System.Collections;
using UnityEngine;
using DG.Tweening;
using Orchard.GameSpace;

namespace Orchard.Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(SwappingPieces))]
    [RequireComponent(typeof(PieceAnimations))]
    public abstract class Piece : MonoBehaviour
    {
        [SerializeField] protected DataBoardObject _dataBoardObject;

        public TypeBoardObject Type { get; private set; }

        public SwappingPieces SwappingPieces { get; private set; }
        public PieceAnimations PieceAnimations { get; private set; }

        public Tile Tile { get; private set; }
        public Tile TileLast { get; private set; }

        public Tweener Tween { get; private set; }

        protected SpriteRenderer _render;

        private Coroutine _coroutineAcceleration;

        protected float _fallTime = 0f;
        protected float _timeDestroyPiece;

        private void Awake()
        {
            SwappingPieces = GetComponent<SwappingPieces>();
            PieceAnimations = GetComponent<PieceAnimations>();
            _render = GetComponent<SpriteRenderer>();

            _timeDestroyPiece = GameManager.Config.TIME_PIECE_DESTROY_ONE + GameManager.Config.TIME_PIECE_DESTROY_TWO;
        }

        public virtual void SetType(TypeBoardObject type)
        {
            Type = type;
        }

        public abstract bool CheckInputType(TypeBoardObject type);

        public abstract bool CheckSwap();

        public virtual bool TapActivation()
        {
            return false;
        }

        public virtual void ChangeTask()
        {
            Tile.Board.TasksLevelInformation.DecreaseToTask(Type);
        }

        public void SetParentTile(Tile tile)
        {
            TileLast = Tile;
            Tile = tile;

            tile.SetPiece(this);

            DOKill();

            transform.SetParent(tile.transform);
            transform.localScale = Vector3.one;
        }

        public virtual void HitPiece()
        {
            Tile.Actions.TileActivities.NewAction(() =>
            {
                _fallTime = 0f;

                ChangeTask();

                PieceAnimations.Destroy(() =>
                {
                    DOKill();
                    GetComponent<PoolObjectBoardObject>().ReturnUnitToPool();
                });

                Tile.Board.ParticleObjectPool.GetUnit(Type)?.Play(transform.position);

                Tile.SetPiece(null);

            }, TypeActions.PieceHit, 0, true, _timeDestroyPiece);
        }

        public virtual void HitNear()
        {

        }

        public virtual void ShufflePiece(TypeBoardObject newTypePiece)
        {
            GetComponent<PoolObjectBoardObject>().ReturnUnitToPool();

            Piece piece = Tile.Board.GetPiece(newTypePiece);
            piece.SetType(newTypePiece);
            piece.SetParentTile(Tile);
            piece.transform.localPosition = Vector3.zero;
            piece.transform.localScale = Vector2.zero;
            piece.gameObject.SetActive(true);
        }

        public void FallingToCenter()
        {
            Tile.Actions.TileActivities.NewAction(() =>
            {
                Tween = transform.DOLocalMove(Vector2.zero, GameManager.Config.TIME_PIECE_FALL).SetEase(Ease.Linear).OnComplete(delegate
                {
                    Tile.Actions.TileActivities.StopAction(TypeActions.PieceMoving, true);
                    StartCoroutine(FallEnd());
                });

                if (_fallTime == 0f)
                    Tween.timeScale = 1f;

            }, TypeActions.PieceMoving, 0, false, GameManager.Config.TIME_PIECE_FALL);

            if (_coroutineAcceleration != null)
                StopCoroutine(_coroutineAcceleration);

            _coroutineAcceleration = StartCoroutine(Acceleration());
        }

        private IEnumerator Acceleration()
        {
            while (Tile.IsBusy)
            {
                yield return null;
                _fallTime += Time.deltaTime * 4.5f;
                Tween.timeScale = 1f + _fallTime;
            }
        }

        private IEnumerator FallEnd()
        {
            SwappingPieces.SetLastMoveTime();

            yield return null;

            if (!Tile.IsBusy)
            {
                float bounce = Mathf.Pow(Tween.timeScale / 20f, 0.85f);

                transform.DOLocalMoveY(-bounce / 2f, bounce / 2f).SetEase(Ease.OutSine, 1f).OnComplete(delegate
                {
                    transform.DOLocalMoveY(bounce / 4f, bounce).SetEase(Ease.InOutSine, 1f).OnComplete(delegate
                    {
                        transform.DOLocalMoveY(0, bounce).SetEase(Ease.InOutSine).OnComplete(delegate
                        {
                            Fall();
                        });
                    });
                });

                _fallTime = 0f;
            }
        }

        protected void DOKill()
        {
            transform.DOKill();
        }

        public virtual void Fall() { }
    }
}
