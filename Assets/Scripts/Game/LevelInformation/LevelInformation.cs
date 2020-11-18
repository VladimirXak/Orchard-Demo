using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Orchard.GameSpace;
using Orchard.UI;

namespace Orchard.Game
{
    public class LevelInformation : MonoBehaviour
    {
        [SerializeField] private StartTaskPanel _startTaskPanel;
        [SerializeField] private TasksLevelInformation _tasksLevelInformation;
        [SerializeField] private MovesMatch _movesMatch;
        [SerializeField] private TextMeshProUGUI _tmpNumberLevel;

        public MovesMatch MovesMatch => _movesMatch;

        public int NumberLevel { get; private set; }

        public void Init(int countMoves, JsonDataTasksLevel dataTasksLevel, int numberLevel)
        {
            NumberLevel = numberLevel;
            MovesMatch.Init(countMoves + GameManager.LevelLoadingData.ExtraMoves);

            _tmpNumberLevel.text = $"lvl: {numberLevel}";

            foreach (var jsonDataTask in dataTasksLevel.listJsonTaskData)
            {
                Sprite sprite = ResourceLoader.LoadSprite(jsonDataTask.typeTask);
                TypeTask typeTask = ResourceLoader.GetTypeTask(jsonDataTask.typeTask);

                DataTask dataTask = new DataTask(sprite, jsonDataTask.countTask, typeTask);

                _tasksLevelInformation.CreateTask(dataTask);
                _startTaskPanel.CreateTaskDisplayer(dataTask);
            }
        }

        public List<DataTask> GetDataTask()
        {
            return _tasksLevelInformation.GetDataTask();
        }

        public bool CheckCompleteTask()
        {
            return _tasksLevelInformation.CheckCompleteTask();
        }
    }
}
