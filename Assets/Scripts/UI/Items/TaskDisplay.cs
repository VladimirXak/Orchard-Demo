using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Orchard.UI
{
    public class TaskDisplay : MonoBehaviour
    {
        [SerializeField] private Image _imgTask;
        [SerializeField] private Image _imgCompleteTask;
        [SerializeField] private TextMeshProUGUI _tmpCountTask;

        public void SetInfo(DataTask dataTask, bool isCheckCompleted = false)
        {
            _imgTask.sprite = dataTask.Sprite;
            _tmpCountTask.text = dataTask.Count.ToString();

            if (isCheckCompleted && dataTask.Count == 0)
            {
                _tmpCountTask.gameObject.SetActive(false);

                _imgCompleteTask.gameObject.SetActive(true);
                _imgCompleteTask.fillAmount = 1;
            }
        }

        public Sprite GetSpriteTask()
        {
            return _imgTask.sprite;
        }

        public void ChangeTask(int countTask)
        {
            if (countTask == 0)
            {
                _tmpCountTask.gameObject.SetActive(false);
                StartCoroutine(AppearanceImageTaskDone());
            }
            else
            {
                _tmpCountTask.text = countTask.ToString();
            }
        }

        private IEnumerator AppearanceImageTaskDone()
        {
            _imgCompleteTask.gameObject.SetActive(true);

            float speedAppearance = 2.5f;

            yield return new WaitForSeconds(0.3f);

            while (_imgCompleteTask.fillAmount != 1)
            {
                _imgCompleteTask.fillAmount += Time.deltaTime * speedAppearance;
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);

            Transform trImgTaskCompleted = _imgCompleteTask.transform;

            trImgTaskCompleted.DOScale(new Vector2(0.5f, 0.5f), 0.4f).SetEase(Ease.OutSine, 1f).OnComplete(delegate
            {
                trImgTaskCompleted.DOScale(new Vector2(1f, 1f), 0.4f).SetEase(Ease.InOutSine, 1f).OnComplete(delegate
                {
                    trImgTaskCompleted.DOScale(new Vector2(0.75f, 0.75f), 0.2f).SetEase(Ease.InOutSine, 1f).OnComplete(delegate
                    {
                        trImgTaskCompleted.DOScale(new Vector2(1f, 1f), 0.2f).SetEase(Ease.InOutSine, 1f);
                    });
                });
            });
        }
    }
}
