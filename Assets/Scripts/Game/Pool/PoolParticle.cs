using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orchard.Game
{
    [RequireComponent(typeof(ParticleSystem))]
    public abstract class PoolParticle : MonoBehaviour
    {
        protected ParticleSystem _particle;
        protected Queue<PoolParticle> _poolReturn;

        bool _isPlaying;

        protected virtual void Awake()
        {
            _particle = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (_isPlaying)
            {
                if (!_particle.isPlaying)
                {
                    gameObject.SetActive(false);
                    _poolReturn.Enqueue(this);
                    _isPlaying = false;
                    return;
                }
            }

            if (_particle.isPlaying)
            {
                _isPlaying = true;
            }
        }

        public void Init(Queue<PoolParticle> poolReturn)
        {
            _poolReturn = poolReturn;
            gameObject.SetActive(false);
        }

        public virtual void InitMaterial(TypeBoardObject type)
        {

        }

        public abstract IBoardObjectChecking GetTypeChecking();

        public virtual void Play(Vector2 position)
        {
            transform.position = position;
            gameObject.SetActive(true);

            _particle.Play();
        }
    }
}
