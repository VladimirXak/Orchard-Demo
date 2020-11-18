using System.Collections.Generic;
using UnityEngine;
using System;

namespace Orchard.Game
{
    public class PieceObjectPool : MonoBehaviour
    {
        [SerializeField] private List<Piece> _listPrefabsUnit;

        private List<PoolBoardObject> _listPoolBoardObject;

        private void Awake()
        {
            _listPoolBoardObject = new List<PoolBoardObject>();

            foreach (var prefUnit in _listPrefabsUnit)
            {
                GameObject parentPool = new GameObject();
                parentPool.transform.SetParent(transform);
                parentPool.name = $"Pool{prefUnit.name}";

                PoolBoardObject poolBoardObject = new PoolBoardObject(prefUnit, parentPool.transform);

                _listPoolBoardObject.Add(poolBoardObject);
            }
        }

        public virtual GameObject GetUnit(TypeBoardObject typeBoardObject)
        {
            foreach (var poolBoardObject in _listPoolBoardObject)
            {
                if (poolBoardObject.CheckInputType(typeBoardObject))
                    return GetUnit(poolBoardObject);
            }

            throw new Exception($"PoolGroupObject.cs. Отсутствует элемент пула {typeBoardObject.ToString()}");
        }

        private GameObject GetUnit(PoolBoardObject pool)
        {
            if (pool.Units.Count == 0)
                AddNewUnit(pool);

            return pool.Units.Dequeue();
        }

        private void AddNewUnit(PoolBoardObject pool)
        {
            GameObject goUnit = Instantiate(pool.PrefUnit, pool.Parent);
            goUnit.SetActive(false);

            goUnit.AddComponent<PoolObjectBoardObject>().SetObjectPooling(pool);

            pool.Units.Enqueue(goUnit);
        }
    }

    public class PoolBoardObject
    {
        public Piece Piece { get; private set; }
        public GameObject PrefUnit { get; private set; }
        public Transform Parent { get; private set; }

        public Queue<GameObject> Units { get; private set; } = new Queue<GameObject>();

        public PoolBoardObject(Piece piece, Transform parent)
        {
            Piece = piece;
            PrefUnit = piece.gameObject;
            Parent = parent;
        }

        public bool CheckInputType(TypeBoardObject type)
        {
            return Piece.CheckInputType(type);
        }

        public void ReturnUnitToPool(GameObject unit)
        {
            unit.SetActive(false);
            unit.transform.SetParent(Parent);

            Units.Enqueue(unit);
        }
    }
}
