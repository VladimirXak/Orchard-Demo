[System.Serializable]
public struct SecureInt
{
    private int value;
    private int offset;

    public SecureInt(int value)
    {
        offset = UnityEngine.Random.Range(int.MinValue / 4, int.MaxValue / 4);
        this.value = value ^ offset;
    }

    public static implicit operator int(SecureInt secureInt)
    {
        return secureInt.value ^ secureInt.offset;
    }

    public static implicit operator SecureInt(int @int)
    {
        return new SecureInt(@int);
    }

    public override string ToString()
    {
        return ((int)this).ToString();
    }
}
