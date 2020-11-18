using UnityEngine;
using TMPro;
using Orchard.GameSpace;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizationTmp : MonoBehaviour, ILocalizationItem
{
    [SerializeField] private string keyLocalization;

    private TextMeshProUGUI _tmp;

    public string Key => keyLocalization;

    private void Awake()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        ChangeLocalization(GameManager.Localization.GetValue(Key));
    }

    public void ChangeLocalization(string value)
    {
        if (_tmp != null)
            _tmp.text = value;
    }
}
