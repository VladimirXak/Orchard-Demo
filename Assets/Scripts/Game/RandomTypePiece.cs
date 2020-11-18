using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orchard.Game
{
    public class RandomTypePiece
    {
        private List<DataCountTypeBoardObject> _listDataRandomPieces;
        private List<DataCountTypeBoardObject> _listDataRandomPiecesBoard;

        private int _countTypes;
        private int _countTypesBoard;

        private List<QueueTypeBoardObject> _listQueueTypeBoardObject;

        public RandomTypePiece(List<JsonDataRandomPieces> listJsonDataRandomPieces)
        {
            _listQueueTypeBoardObject = new List<QueueTypeBoardObject>();

            IBoardObjectChecking generalPieces = new PieceGeneralChecking();

            _listDataRandomPieces = new List<DataCountTypeBoardObject>();
            _listDataRandomPiecesBoard = new List<DataCountTypeBoardObject>();

            foreach (var jsonDataRandomPieces in listJsonDataRandomPieces)
            {
                TypeBoardObject type = ResourceLoader.GetTypePiece(jsonDataRandomPieces.typePiece);
                _countTypes += jsonDataRandomPieces.chance;

                _listDataRandomPieces.Add(new DataCountTypeBoardObject(type, _countTypes));

                if (generalPieces.Check(type))
                {
                    _countTypesBoard += jsonDataRandomPieces.chance;
                    _listDataRandomPiecesBoard.Add(new DataCountTypeBoardObject(type, _countTypesBoard));
                }
            }
        }

        public TypeBoardObject GetRandomTypePiece()
        {
            if (_listQueueTypeBoardObject.Count != 0)
            {
                if (_listQueueTypeBoardObject[0].Count != 0)
                {
                    _listQueueTypeBoardObject[0].Count--;
                }
                else
                {
                    TypeBoardObject type = _listQueueTypeBoardObject[0].Type;
                    _listQueueTypeBoardObject.RemoveAt(0);
                    return type;
                }
            }

            int random = Random.Range(0, _countTypes);

            foreach (var dataRandomPieces in _listDataRandomPieces)
            {
                if (random < dataRandomPieces.count)
                    return dataRandomPieces.typeBoardObject;
            }

            return _listDataRandomPieces[0].typeBoardObject;
        }

        public TypeBoardObject GetRandomTypePieceBoard()
        {
            int random = Random.Range(0, _countTypesBoard);

            foreach (var dataRandomPieces in _listDataRandomPiecesBoard)
            {
                if (random < dataRandomPieces.count)
                    return dataRandomPieces.typeBoardObject;
            }

            return _listDataRandomPiecesBoard[0].typeBoardObject;
        }

        public void AddQueuePiece(QueueTypeBoardObject queueTypeBoardObject)
        {
            _listQueueTypeBoardObject.Add(queueTypeBoardObject);
        }
    }

    public class QueueTypeBoardObject
    {
        public TypeBoardObject Type;
        public int Count;

        public QueueTypeBoardObject(TypeBoardObject type, int count)
        {
            Count = count;
            Type = type;
        }
    }
}
