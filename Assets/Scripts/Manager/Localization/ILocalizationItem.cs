using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILocalizationItem
{
    string Key { get;}
    void ChangeLocalization(string value);
}
