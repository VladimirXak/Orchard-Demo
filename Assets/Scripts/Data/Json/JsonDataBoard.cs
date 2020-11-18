using System.Collections.Generic;

namespace Orchard
{
    [System.Serializable]
    public class JsonDataBoard
    {
        public JsonTileDetail[,] tilesDetail;
        public List<DataTask> listTypeTask;

        public int widthBoard;
        public int heightBoard;
        public int countMoves;

        public JsonDataBoard()
        {
            listTypeTask = new List<DataTask>();
        }
    }

    [System.Serializable]
    public class JsonTileDetail
    {
        public PosXY posXY;
        public PosXY direction;

        public TypeBoardObject baseTypePiece;
        public TypeBoardObject typePiece;

        public bool isEnable = true;
        public bool isSpawnerPieces;
    }
}
