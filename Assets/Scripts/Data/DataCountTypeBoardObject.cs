namespace Orchard.Game
{
    public class DataCountTypeBoardObject
    {
        public TypeBoardObject typeBoardObject;
        public int count;

        public DataCountTypeBoardObject(TypeBoardObject typeBoardObject, int count)
        {
            this.typeBoardObject = typeBoardObject;
            this.count = count;
        }
    }
}