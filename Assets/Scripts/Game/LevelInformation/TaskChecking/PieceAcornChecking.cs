namespace Orchard
{
    public class PieceAcornChecking : IBoardObjectChecking
    {
        public bool Check(TypeBoardObject type)
        {
            switch (type)
            {
                case TypeBoardObject.PieceAcornOne:
                case TypeBoardObject.PieceAcornTwo:
                    return true;
            }

            return false;
        }
    }
}
