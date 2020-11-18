using UnityEngine;

namespace Orchard.UI
{
    public class StartTaskPanel : MonoBehaviour
    {
        [SerializeField] private TaskDisplay _prefTaskDisplayer;
        [SerializeField] private Transform _parent;

        public void CreateTaskDisplayer(DataTask dataTask)
        {
            TaskDisplay taskDisplayer = Instantiate(_prefTaskDisplayer, _parent);
            taskDisplayer.SetInfo(dataTask);
        }
    }
}
