using UnityEngine;

public class SwitchGameObject : SwitchTwoObject
{
    [SerializeField] private GameObject first;
    [SerializeField] private GameObject second;

    public override void Switch(bool isFirst)
    {
        first.SetActive(isFirst);
        second.SetActive(!isFirst);
    }
}
