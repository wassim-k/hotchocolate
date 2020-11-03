#if STITCHING
namespace HotChocolate.Stitching.Transport
#else
namespace StrawberryShake.Transport
#endif
{
    public enum SocketCloseStatus
    {
        None,
        NormalClosure,
        EndpointUnavailable,
        ProtocolError,
        InvalidMessageType,
        InvalidPayloadData,
        PolicyViolation,
        MessageTooBig,
        MandatoryExtension,
        InternalServerError
    }
}
