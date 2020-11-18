using UnityEngine;

namespace Orchard
{
    public class DataTask
    {
        public Sprite Sprite { get; private set; }
        public int Count { get; private set; }
        public TypeTask TypeTask { get; private set; }

        public DataTask(Sprite sprite, int count, TypeTask typeTask = TypeTask.NULL)
        {
            Sprite = sprite;
            Count = count;
            TypeTask = typeTask;
        }
    }
}