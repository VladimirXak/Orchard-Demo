using System.Collections.Generic;

namespace Orchard.Game
{
    public class GeneralPiece : Piece
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
            List<List<PosXY>> combos = Tile.Board.ComboOfPieces.FindCombos();

            if (combos.Count > 0)
            {
                foreach (var combo in combos)
                {
                    if (combo.Contains(Tile.PosXY) || combo.Contains(TileLast.PosXY))
                    {
                        Tile.Board.ComboOfPieces.ComboActivator.ActivateCombo(combos);
                        TileLast.Piece?.CheckSwap();
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
