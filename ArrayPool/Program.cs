// ArrayPool<T> Class
// https://learn.microsoft.com/en-us/dotnet/api/system.buffers.arraypool-1?view=netcore-3.0

using System.Buffers;

const int COLLECTION_SIZE = 1_000_000;

SharedPoolWithoutClearingArray();
SharedPoolWithClearingArray();
CustomPoolWithCaching();

// public abstract void Return (T[] array, bool clearArray = false);
// https://learn.microsoft.com/en-us/dotnet/api/system.buffers.arraypool-1.return?view=netcore-3.0

void SharedPoolWithoutClearingArray()
{
    var sharedPool = ArrayPool<int>.Shared;
    var array = sharedPool.Rent(COLLECTION_SIZE);
    for (int i = 0; i < COLLECTION_SIZE; i++)
    {
        array[i] = i;
    }

    sharedPool.Return(array, false);
}
void SharedPoolWithClearingArray()
{
    var sharedPool = ArrayPool<int>.Shared;
    var array = sharedPool.Rent(COLLECTION_SIZE);
    for (int i = 0; i < COLLECTION_SIZE; i++)
    {
        array[i] = i;
    }

    sharedPool.Return(array, true);
}

// ArrayPool<T>.Create Method
// https://learn.microsoft.com/en-us/dotnet/api/system.buffers.arraypool-1.create?view=net-8.0

// ArrayPool<T>.Rent(Int32) Method
// https://learn.microsoft.com/en-us/dotnet/api/system.buffers.arraypool-1.rent?view=net-8.0

void CustomPoolWithCaching()
{
    ArrayPool<int> _cachedPool = ArrayPool<int>.Create();
    var array = _cachedPool.Rent(COLLECTION_SIZE);
    for (int i = 0; i < COLLECTION_SIZE; i++)
    {
        array[i] = i;
    }
    _cachedPool.Return(array, false);
}
