using System.Collections.Generic;
using UnityEngine;

namespace Orchard.Game
{
    public class ParticleObjectPool : MonoBehaviour
    {
        [SerializeField] private List<PoolParticle> _listPrefParticles;

        private List<DataPoolParticle> _pool;

        private void Awake()
        {
            _pool = new List<DataPoolParticle>();

            foreach (var particle in _listPrefParticles)
            {
                DataPoolParticle newPool = new DataPoolParticle()
                {
                    prefParticle = particle,
                    typeChecking = particle.GetTypeChecking(),
                    pool = new Queue<PoolParticle>(),
                };

                _pool.Add(newPool);
            }

            _listPrefParticles = null;
        }

        public PoolParticle GetUnit(TypeBoardObject type)
        {
            foreach (var itemPool in _pool)
            {
                if (itemPool.typeChecking.Check(type))
                {
                    if (itemPool.pool.Count == 0)
                        AddUnit(itemPool.prefParticle, itemPool.pool);

                    var unit = itemPool.pool.Dequeue();
                    unit.InitMaterial(type);
                    return unit;
                }
            }

            return null;
        }

        private void AddUnit(PoolParticle prefParticle, Queue<PoolParticle> pool)
        {
            PoolParticle newParticle = Instantiate(prefParticle, transform);
            newParticle.Init(pool);

            pool.Enqueue(newParticle);
        }

        private struct DataPoolParticle
        {
            public PoolParticle prefParticle;
            public IBoardObjectChecking typeChecking;
            public Queue<PoolParticle> pool;
        }
    }
}
