using System;
using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class RepositoryException : Exception
    {
        public RepositoryException()
        {
        }

        public RepositoryException(string message) : base(message)
        {
        }

        public RepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        [Obsolete("Mitigação devido a upgrade do .net", DiagnosticId = "SYSLIB0051")]
        protected RepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}