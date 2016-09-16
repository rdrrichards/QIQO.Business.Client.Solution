using System;
using System.Runtime.Serialization;

namespace QIQO.Business.Client.Core
{
    [Serializable]
    public class AuthorizationValidationException : Exception
    {
        public AuthorizationValidationException()
        {
        }

        public AuthorizationValidationException(string message)
            : base(message)
        {
        }

        public AuthorizationValidationException(string message, Exception exception)
            : base(message, exception)
        {
        }

        public AuthorizationValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
