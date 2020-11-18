using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orchard.Game
{
    public class TileSelect : MonoBehaviour
    {
        [SerializeField] private GameObject _goFrame;
        private SpriteRenderer _render;

        private Coroutine _coroutineIncOpacity;
        private Coroutine _coroutineDecOpacity;
        private Coroutine _coroutineBlink;

        private Color _color;
        private float _maxOpasity = 1f;
        private float _minOpasity = 0.3f;
        private float _speed = 1f;

        private void Awake()
        {
            _render = _goFrame.GetComponent<SpriteRenderer>();
            _color = _render.color;

            _render.color = new Color(_color.r, _color.g, _color.b, _minOpasity);
        }

        public void StartSelect()
        {
            _goFrame.SetActive(true);

            if (_coroutineDecOpacity != null)
                StopCoroutine(_coroutineDecOpacity);

            _coroutineBlink = StartCoroutine(CoroutineBlink());
        }

        public void StopSelect()
        {
            if (_coroutineBlink != null)
                StopCoroutine(_coroutineBlink);

            if (_coroutineIncOpacity != null)
                StopCoroutine(_coroutineIncOpacity);

            if (_goFrame.activeSelf)
                _coroutineDecOpacity = StartCoroutine(CoroutineHide());
        }

        private IEnumerator CoroutineBlink()
        {
            while (true)
            {
                yield return _coroutineIncOpacity = StartCoroutine(CoroutineIncOpacity());
                yield return new WaitForSeconds(0.1f);
                yield return _coroutineDecOpacity = StartCoroutine(CoroutineDecOpacity());
                yield return new WaitForSeconds(0.1f);
            }
        }

        private IEnumerator CoroutineIncOpacity()
        {
            while (_render.color.a < _maxOpasity)
            {
                _render.color = new Color(_color.r, _color.g, _color.b, _render.color.a + (Time.deltaTime * _speed));
                yield return null;
            }
        }

        private IEnumerator CoroutineDecOpacity()
        {
            while (_render.color.a > _minOpasity)
            {
                _render.color = new Color(_color.r, _color.g, _color.b, _render.color.a - (Time.deltaTime * _speed));
                yield return null;
            }
        }

        private IEnumerator CoroutineHide(bool isHide = false)
        {
            float speedHide = _speed * 2;

            while (_render.color.a > 0)
            {
                _render.color = new Color(_color.r, _color.g, _color.b, _render.color.a - (Time.deltaTime * speedHide));
                yield return null;
            }

            _goFrame.SetActive(false);
        }
    }
}
