using UnityEngine;

namespace Orchard.Game
{
    public class CameraSize : MonoBehaviour
    {
        [SerializeField] private Transform _extremeTopDot;

        private const float _extremeHorizontalWorldPoint = 5.5f;
        private const float _extremeVerticalWorldPoint = 6.6f;

        private void Awake()
        {
            Camera camera = Camera.main;
            float worldPointX = camera.ViewportToWorldPoint(new Vector2(1, 0)).x;
            camera.orthographicSize -= 1;
            float dWorldPointX = Mathf.Abs(worldPointX - camera.ViewportToWorldPoint(new Vector2(1, 0)).x);
            camera.orthographicSize = _extremeHorizontalWorldPoint / dWorldPointX;
        }

        private void Start()
        {
            if (_extremeTopDot.position.y < _extremeVerticalWorldPoint)
            {
                Camera camera = Camera.main;

                float viewPortPointY = camera.WorldToViewportPoint(_extremeTopDot.position).y;
                float worldPointY = _extremeTopDot.position.y;
                camera.orthographicSize -= 1;

                float dWorldPointY = Mathf.Abs(worldPointY - camera.ViewportToWorldPoint(new Vector2(0, viewPortPointY)).y);

                camera.orthographicSize = _extremeVerticalWorldPoint / dWorldPointY;
            }
        }
    }
}
