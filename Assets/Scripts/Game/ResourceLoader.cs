using UnityEngine;
using System;

namespace Orchard
{
    public static class ResourceLoader
    {
        public static string GetName(TypeBoardObject typeBoardObject)
        {
            return typeBoardObject.ToString();
        }

        public static TypeBoardObject GetTypePiece(string name)
        {
            foreach (TypeBoardObject type in Enum.GetValues(typeof(TypeBoardObject)))
            {
                if (type.ToString() == name)
                    return type;
            }

            return TypeBoardObject.NULL;
        }

        public static TypeTask GetTypeTask(string name)
        {
            foreach (TypeTask type in Enum.GetValues(typeof(TypeTask)))
            {
                if (type.ToString() == name)
                    return type;
            }

            return TypeTask.NULL;
        }

        public static Sprite LoadSprite(string imgName)
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>("MatchImgs/AtlasBoardObjects");

            foreach (var sprite in sprites)
            {
                if (sprite.name == imgName)
                    return sprite;
            }

            return null;
        }
    }
}
