using System.Collections.Generic;
using System;

[Serializable]
public struct DataLocalization
{
    public List<DataItemLocalization> listDataItems;
}

[Serializable]
public struct DataItemLocalization
{
    public string key;
    public string value;
}
