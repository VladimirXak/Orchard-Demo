using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orchard.Game
{
    [RequireComponent(typeof(Tile))]
    public class TileActivities : MonoBehaviour
    {
        private Tile _tile;

        public Dictionary<TypeActions, Coroutine> Actions { get; } = new Dictionary<TypeActions, Coroutine>();

        private void Awake()
        {
            _tile = GetComponent<Tile>();
        }

        public void NewAction(Action act, TypeActions typeAction, float delay = 0f, bool waiting = false, float finishDelay = 0f, bool nextMove = true)
        {
            if (!Actions.ContainsKey(typeAction))
            {
                if (Actions.Count == 0)
                {
                    _tile.Board.ActionTiles.Add(_tile);
                }

                Actions.Add(typeAction, null);

                Actions[typeAction] = StartCoroutine(Command(act, typeAction, delay, waiting, finishDelay, nextMove));
                _tile.IsBusy = true;
            }
        }

        private IEnumerator Command(Action act, TypeActions typeAction, float delay, bool waiting, float finishDelay, bool nextMove)
        {
            if (waiting)
            {
                float tt = Time.time;

                while (Actions.Count > 1)
                {
                    yield return null;
                }

                delay -= (Time.time - tt);
            }

            if (delay > 0)
                yield return new WaitForSeconds(delay);
            else
            {
                yield return null;
            }

            act();

            if (finishDelay > 0)
            {
                yield return new WaitForSeconds(finishDelay);
            }
            else
            {
                yield return null;
            }

            StopAction(typeAction, nextMove);
        }

        public void StopAction(TypeActions act, bool nextMove)
        {
            if (!Actions.ContainsKey(act))
                return;
            else
            {
                StopCoroutine(Actions[act]);
                Actions.Remove(act);
            }

            if (Actions.Count == 0)
            {
                _tile.IsBusy = false;

                _tile.Board.AddTileFall(_tile);

                _tile.Board.ActionTiles.Remove(_tile);

                if (nextMove)
                {
                    _tile.Board.NextMove();
                }
            }
        }
    }
}
