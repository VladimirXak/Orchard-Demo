namespace Orchard
{
    public class PieceOrangeChecking : IBoardObjectChecking
    {
        public bool Check(TypeBoardObject tile)
        {
            return tile == TypeBoardObject.PieceOrange;
        }
    }
}
