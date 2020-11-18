namespace Orchard
{
    public class PieceRedChecking : IBoardObjectChecking
    {
        public bool Check(TypeBoardObject type)
        {
            return type == TypeBoardObject.PieceRed;
        }
    }
}
