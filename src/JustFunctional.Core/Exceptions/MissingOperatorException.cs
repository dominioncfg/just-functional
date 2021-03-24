using System;
namespace JustFunctional.Core
{
    [Serializable]
    public class MissingOperatorException : JustFunctionalBaseException
    {
        public MissingOperatorException() { }
        public MissingOperatorException(string message) : base(message) { }
        public MissingOperatorException(string message, Exception inner) : base(message, inner) { }
        protected MissingOperatorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}