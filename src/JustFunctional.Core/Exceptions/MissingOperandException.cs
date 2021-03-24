using System;
namespace JustFunctional.Core
{
    [Serializable]
    public class MissingOperandException : JustFunctionalBaseException
    {
        public MissingOperandException() { }
        public MissingOperandException(string message) : base(message) { }
        public MissingOperandException(string message, Exception inner) : base(message, inner) { }
        protected MissingOperandException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}