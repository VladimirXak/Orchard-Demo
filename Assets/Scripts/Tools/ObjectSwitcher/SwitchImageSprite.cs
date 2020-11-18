using UnityEngine;
using UnityEngine.UI;

public class SwitchImageSprite : SwitchTwoObject
{
    [SerializeField] private Image _image;
    [Space(10)]
    [SerializeField] private Sprite _firstSprite;
    [SerializeField] private Sprite _secondSprite;

    private void Awake()
    {
        _image.sprite = _firstSprite;
    }

    public override void Switch(bool isFirst)
    {
        _image.sprite = isFirst ? _firstSprite : _secondSprite;
    }
}
