using Orchard.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Orchard.Game
{
    public class TasksLevelInformation : MonoBehaviour
    {
        [SerializeField] private TaskDisplay _prefTaskDisplay;
        [SerializeField] private Transform _parentTasks;
        [Space(10)]
        [SerializeField] private EndingLevel _endingLevel;

        private List<TaskLevel> _listTaskLevel = new List<TaskLevel>();

        public void CreateTask(DataTask dataTask)
        {
            TaskDisplay taskDisplayer = Instantiate(_prefTaskDisplay, _parentTasks);
            taskDisplayer.SetInfo(dataTask);

            TaskLevel taskLevel = new TaskLevel(dataTask, taskDisplayer);

            _listTaskLevel.Add(taskLevel);
        }

        public bool CheckCompleteTask()
        {
            foreach (var task in _listTaskLevel)
            {
                if (!task.IsCompleted)
                    return false;
            }

            return true;
        }

        public bool CheckTask(TypeBoardObject type)
        {
            foreach (var task in _listTaskLevel)
            {
                if (task.CheckTask(type))
                    return true;
            }

            return false;
        }

        public void DecreaseToTask(TypeBoardObject type)
        {
            foreach (var task in _listTaskLevel)
            {
                if (task.DecreaseToTask(type))
                {
                    if (CheckCompleteTask())
                        _endingLevel.EndLevel(true);

                    return;
                }
            }
        }

        public void AddToTask(TypeBoardObject type)
        {
            foreach (var task in _listTaskLevel)
            {
                if (task.AddToTask(type))
                    return;
            }
        }

        public List<DataTask> GetDataTask()
        {
            List<DataTask> listDataTask = new List<DataTask>();

            foreach (var taskLevel in _listTaskLevel)
                listDataTask.Add(taskLevel.GetDataTaskInfo());

            return listDataTask;
        }
    }
}
