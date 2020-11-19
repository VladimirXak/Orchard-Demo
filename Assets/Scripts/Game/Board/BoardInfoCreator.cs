using Orchard.GameSpace;
using System.Collections.Generic;
using UnityEngine;

namespace Orchard.Game
{
    public class BoardInfoCreator : MonoBehaviour
    {
        [SerializeField] private MoveOfPieces _moveOfPieces;
        [SerializeField] private ComboOfPieces _comboOfPieces;
        [SerializeField] private LevelInformation _levelInformation;

        public JsonDataBoard CreateBoardInfo(ref RandomTypePiece randomTypePiece)
        {
            LoadLevel loadLevel = new LoadLevel();

            JsonSavedLevelData savedLevelData = loadLevel.GetLevelData(GameManager.LevelLoadingData.NumberLevel);

            if (savedLevelData.numberLevel != GameManager.LevelLoadingData.NumberLevel)
                throw new System.Exception("Ошибка загрузки уровня. Файловый уровень не совпадает с текущим уровнем.");

            JsonDataBoard boardInfo = new JsonDataBoard()
            {
                widthBoard = savedLevelData.widthBoard,
                heightBoard = savedLevelData.heightBoard,
                countMoves = savedLevelData.countMoves,
            };

            foreach (var taskData in savedLevelData.dataLevelTasks.listJsonTaskData)
            {
                boardInfo.listTypeTask.Add(new DataTask(null, taskData.countTask, ResourceLoader.GetTypeTask(taskData.typeTask)));
            }

            randomTypePiece = new RandomTypePiece(savedLevelData.generalRandomPieces);

            boardInfo.tilesDetail = FillTilesDetail(boardInfo, savedLevelData.data);

            bool isHaveMove = false;

            for (int i = 0; i < 50; i++)
            {
                ChangeRandomPieces(randomTypePiece, boardInfo);

                if (IsRidOfCompos(randomTypePiece, boardInfo))
                {
                    List<List<PosXY>> moves = _moveOfPieces.FindMoves(boardInfo);

                    if (moves.Count != 0)
                    {
                        isHaveMove = true;
                        break;
                    }
                }
            }

            if (!isHaveMove)
            {
                throw new System.Exception("Ошибка генерации уровня");
            }

            _levelInformation.Init(savedLevelData.countMoves, savedLevelData.dataLevelTasks, savedLevelData.numberLevel);

            return boardInfo;
        }

        private JsonTileDetail[,] FillTilesDetail(JsonDataBoard boardInfo, List<JsonSavedTileData> listSavedTileData)
        {
            JsonTileDetail[,] tilesDetail = new JsonTileDetail[boardInfo.widthBoard, boardInfo.heightBoard];

            for (int i = 0; i < tilesDetail.GetLength(0); i++)
            {
                for (int j = 0; j < tilesDetail.GetLength(1); j++)
                {
                    JsonTileDetail tile = new JsonTileDetail();

                    JsonSavedTileData tileData = listSavedTileData.Find(data => data.posXY.Equals(new PosXY(i, j)));

                    tile.posXY = tileData.posXY;

                    if (tileData == null)
                        continue;

                    if (tileData.direction.Equals(new PosXY(0, 0)))
                        tile.direction = new PosXY(0, 1);
                    else
                        tile.direction = tileData.direction;

                    tile.baseTypePiece = ResourceLoader.GetTypePiece(tileData.typePiece);
                    tile.isEnable = tileData.isEnable;
                    tile.isSpawnerPieces = tileData.isSpawnerPieces;

                    tilesDetail[i, j] = tile;
                }
            }

            return tilesDetail;
        }

        private void ChangeRandomPieces(RandomTypePiece randomTypePiece, JsonDataBoard boardInfo)
        {
            for (int x = 0; x < boardInfo.widthBoard; x++)
            {
                for (int y = 0; y < boardInfo.heightBoard; y++)
                {
                    if (boardInfo.tilesDetail[x, y].baseTypePiece == TypeBoardObject.PieceRnd)
                    {
                        boardInfo.tilesDetail[x, y].typePiece = randomTypePiece.GetRandomTypePieceBoard();
                    }
                    else
                    {
                        boardInfo.tilesDetail[x, y].typePiece = boardInfo.tilesDetail[x, y].baseTypePiece;
                    }
                }
            }
        }

        private bool IsRidOfCompos(RandomTypePiece randomTypePiece, JsonDataBoard boardInfo)
        {
            for (int i = 0; i < 30; i++)
            {
                List<List<PosXY>> combos = _comboOfPieces.FindCombos(boardInfo);

                if (combos.Count > 0)
                {
                    foreach (var combo in combos)
                    {
                        foreach (PosXY posXY in combo)
                        {
                            if (boardInfo.tilesDetail[posXY.x, posXY.y].baseTypePiece == TypeBoardObject.PieceRnd)
                                boardInfo.tilesDetail[posXY.x, posXY.y].typePiece = randomTypePiece.GetRandomTypePieceBoard();
                        }
                    }
                }
                else
                {
                    return true;
                }
            }

            return false;
        }
    }
}
