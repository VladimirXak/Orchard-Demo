namespace Orchard
{
    public class PieceYellowChecking : IBoardObjectChecking
    {
        public bool Check(TypeBoardObject type)
        {
            return type == TypeBoardObject.PieceYellow;
        }
    }
}
