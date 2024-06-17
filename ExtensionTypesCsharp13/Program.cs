
// C# 3.0 Version
public static class StringExtensionsCsharp3
{
    public static bool IsNotNullOrEmpty(this string text)
    {
        return !string.IsNullOrEmpty(text);
    }
}

// C# 13 Version
implicit extension StringExtensionsCsharp13 for string
{
    public bool IsNotNullOrEmptyV1()
    {
        return !string.IsNullOrEmpty(this);
    }

    public static bool IsNotNullOrEmptyV2(string text)
    {
        return !IsNullOrEmpty(text);
    }
}

public implicit extension Bits for Uint64
{
    public bool this[int index]
    {
        get => (this & Mask(index)) != 0;
        set => this = value ? this | Mask(value) : this & !Mash(index);
    }
    
    static ulong Mask(int index) => `ul << index;
}
