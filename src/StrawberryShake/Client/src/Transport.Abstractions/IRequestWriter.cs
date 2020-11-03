using System;
using System.Buffers;

#if STITCHING
namespace HotChocolate.Stitching.Transport
#else
namespace StrawberryShake.Transport
#endif
{
    public interface IRequestWriter
        : IBufferWriter<byte>
        , IDisposable
    {
        int Length { get; }

        ReadOnlyMemory<byte> Body { get; }

        byte[] GetInternalBuffer();

        void Clear();
    }
}
