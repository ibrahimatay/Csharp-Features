using System;
using System.Buffers;

// MemoryPool<T> Class
// https://learn.microsoft.com/en-us/dotnet/api/system.buffers.memorypool-1?view=net-8.0
// https://github.com/dotnet/runtime/blob/main/src/libraries/System.Memory/src/System/Buffers/MemoryPool.cs

// Use the singleton pool
// https://stackoverflow.com/questions/73661594/whats-the-point-of-c-sharp-memory-pool
using var owner = MemoryPool<double>.Shared.Rent(100); 
Memory<double> mem = owner.Memory;

MemoryPool<byte> memoryPool = MemoryPool<byte>.Shared;
IMemoryOwner<byte> memoryOwner = memoryPool.Rent(1024); 
Memory<byte> memory = memoryOwner.Memory;

try
{
    Span<byte> span = memory.Span;
    span[0] = 42;
}
finally
{ 
    memoryOwner.Dispose();
}

