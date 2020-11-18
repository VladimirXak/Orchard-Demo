using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orchard
{
    [System.Serializable]
    public struct DataSpriteBoardObject
    {
        [SerializeField] private TypeBoardObject _typeBoardObject;
        [SerializeField] private Sprite _sprite;

        public TypeBoardObject TypeBoardObject => _typeBoardObject;
        public Sprite Sprite => _sprite;
    }
}
