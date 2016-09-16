using System;
using System.Runtime.Serialization;

namespace QIQO.Business.Client.Core
{
    [Serializable]
    public class NotFoundException : Exception
    {

        public NotFoundException()
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, Exception exception)
            : base(message, exception)
        {
        }

        public NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
