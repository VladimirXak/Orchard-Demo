using DG.Tweening;

namespace Orchard.Game
{
    public class AcornPiece : Piece
    {
        public override void SetType(TypeBoardObject type)
        {
            _render.sprite = _dataBoardObject.GetSprite(type);
            base.SetType(type);
        }

        public override bool CheckInputType(TypeBoardObject type)
        {
            foreach (TypeBoardObject typeBoardObject in _dataBoardObject)
            {
                if (typeBoardObject == type)
                    return true;
            }

            return false;
        }

        public override bool CheckSwap()
        {
            return false;
        }

        public override void HitNear()
        {
            HitPiece();
        }

        public override void HitPiece()
        {
            SoundMatch.Instance.PlayClip(Type, _dataBoardObject.AudioClipDestroy);

            switch (Type)
            {
                case TypeBoardObject.PieceAcornTwo:
                    SetType(TypeBoardObject.PieceAcornOne);
                    AnimationHit();
                    break;
                case TypeBoardObject.PieceAcornOne:
                    base.HitPiece();
                    break;
            }
        }

        private void AnimationHit()
        {
            Tile.Actions.TileActivities.NewAction(() =>
            {
                float startPosY = transform.localPosition.y;

                transform.DOShakeRotation(_timeDestroyPiece, 90, 10, 45);
                transform.DOShakePosition(_timeDestroyPiece, 0.15f, 10, 60);
            },
            TypeActions.PieceHit, 0, true, _timeDestroyPiece);
        }
    }
}
