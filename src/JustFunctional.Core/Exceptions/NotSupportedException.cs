using System;
namespace JustFunctional.Core
{
    [Serializable]
    public class NotSupportedException : JustFunctionalBaseException
    {
        public NotSupportedException() { }
        public NotSupportedException(string message) : base(message) { }
        public NotSupportedException(string message, Exception inner) : base(message, inner) { }
        protected NotSupportedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}