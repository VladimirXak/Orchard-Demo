namespace Orchard
{
    public class PieceGeneralChecking : IBoardObjectChecking
    {
        public bool Check(TypeBoardObject type)
        {
            switch (type)
            {
                case TypeBoardObject.PieceRed:
                case TypeBoardObject.PieceOrange:
                case TypeBoardObject.PieceBlue:
                case TypeBoardObject.PieceYellow:
                case TypeBoardObject.PieceGreen:
                    return true;
            }

            return false;
        }
    }
}
