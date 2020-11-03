using System.Net.Http;

#if STITCHING
namespace HotChocolate.Stitching.Transport
#else
namespace StrawberryShake.Transport
#endif
{
    public static class MessageWriterExtensions
    {
        public static ByteArrayContent ToByteArrayContent(this IRequestWriter writer)
        {
            return new ByteArrayContent(writer.GetInternalBuffer(), 0, writer.Length);
        }
    }
}
