using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Orchard.Game
{
    public class ComboActivator : MonoBehaviour
    {
        [SerializeField] private Board _board;
        [SerializeField] private SoundMatch _soundMatch;

        public void ActivateCombo(List<List<PosXY>> combos)
        {
            foreach (var combo in combos)
            {
                List<Tile> comboTiles = new List<Tile>();

                foreach (PosXY posXY in combo)
                {
                    comboTiles.Add(_board.Tiles[posXY.x, posXY.y]);
                }

                _soundMatch.PlayClip(TypeBoardObject.PieceRnd, null);

                ActivateMatch(comboTiles);
            }

            if (combos.Count == 0)
            {
                _board.ShufflePieces.CheckNeedShuffle();
            }
        }

        private void ActivateMatch(List<Tile> comboTiles)
        {
            foreach (Tile tile in comboTiles)
            {
                tile.Actions.HitMatch();
            }
        }
    }
}
