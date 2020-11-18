using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orchard.Game
{
    public class ActiveSound : MonoBehaviour
    {
        private Dictionary<TypeBoardObject, Coroutine> _actions;

        private void Awake()
        {
            _actions = new Dictionary<TypeBoardObject, Coroutine>();
        }

        public bool AddSound(TypeBoardObject type, float time = 0.15f)
        {
            if (_actions.ContainsKey(type))
                return false;

            _actions.Add(type, null);

            _actions[type] = StartCoroutine(StartCommand(type, time));

            return true;
        }

        private IEnumerator StartCommand(TypeBoardObject type, float time)
        {
            yield return new WaitForSeconds(time);

            _actions.Remove(type);
        }
    }
}
