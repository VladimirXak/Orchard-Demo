using Orchard.UI;
using UnityEngine;

namespace Orchard.Game
{
    public class TaskLevel
    {
        public bool IsCompleted { get; private set; }

        private TaskDisplay _taskDisplayer;

        private SecureInt _countTask;
        private IBoardObjectChecking _taskChecking;

        public TaskLevel(DataTask dataTask, TaskDisplay taskDisplayer)
        {
            this._taskDisplayer = taskDisplayer;
            taskDisplayer.SetInfo(dataTask);

            _taskChecking = TaskCheckingInitializer.GetTaskCheking(dataTask.TypeTask);

            _countTask = dataTask.Count;
        }

        public bool DecreaseToTask(TypeBoardObject type)
        {
            if (CheckTask(type))
            {
                if (_countTask != 0)
                {
                    _countTask--;

                    if (_countTask == 0)
                        IsCompleted = true;

                    _taskDisplayer.ChangeTask(_countTask);
                }

                return true;
            }

            return false;
        }

        public bool AddToTask(TypeBoardObject type)
        {
            if (CheckTask(type))
            {
                _countTask++;

                _taskDisplayer.ChangeTask(_countTask);

                return true;
            }

            return false;
        }

        public bool CheckTask(TypeBoardObject type)
        {
            return _taskChecking.Check(type) && (_countTask != 0);
        }

        public DataTask GetDataTaskInfo()
        {
            Sprite spriteTask = _taskDisplayer.GetSpriteTask();
            return new DataTask(spriteTask, _countTask);
        }
    }
}
