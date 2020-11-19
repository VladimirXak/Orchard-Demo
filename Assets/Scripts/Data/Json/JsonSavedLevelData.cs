using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orchard;

public class JsonSavedLevelData
{
    public JsonSavedLevelData()
    {
        data = new List<JsonSavedTileData>();
        dataLevelTasks = new JsonDataTasksLevel();
    }

    public List<JsonSavedTileData> data;
    public JsonDataTasksLevel dataLevelTasks;

    public int numberLevel;
    public int countMoves;

    public int widthBoard;
    public int heightBoard;

    public List<JsonDataRandomPieces> generalRandomPieces;
}

[System.Serializable]
public class JsonSavedTileData
{
    public PosXY posXY;
    public PosXY direction;

    public bool isEnable;
    public bool isSpawnerPieces;

    public string typePiece;
}

[System.Serializable]
public class JsonDataTasksLevel
{
    public List<JsonTaskData> listJsonTaskData;

    public JsonDataTasksLevel()
    {
        listJsonTaskData = new List<JsonTaskData>();
    }
}

[System.Serializable]
public class JsonTaskData
{
    public string typeTask;
    public int countTask;

    public JsonTaskData(string newTypeTask, int newCountTask)
    {
        typeTask = newTypeTask;
        countTask = newCountTask;
    }
}

[System.Serializable]
public class JsonDataRandomPieces
{
    public string typePiece;
    public int chance;
}