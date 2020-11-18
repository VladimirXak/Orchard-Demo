namespace Orchard
{
    public class PieceGreenChecking : IBoardObjectChecking
    {
        public bool Check(TypeBoardObject type)
        {
            return type == TypeBoardObject.PieceGreen;
        }
    }
}
