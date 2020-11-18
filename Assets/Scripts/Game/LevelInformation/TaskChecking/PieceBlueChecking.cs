namespace Orchard
{
    public class PieceBlueChecking : IBoardObjectChecking
    {
        public bool Check(TypeBoardObject type)
        {
            return type == TypeBoardObject.PieceBlue;
        }
    }
}
