using System;
namespace JustFunctional.Core
{
    [Serializable]
    public class ExpressionIsNullOrEmptyException : JustFunctionalBaseException
    {
        public ExpressionIsNullOrEmptyException() { }
        public ExpressionIsNullOrEmptyException(string message) : base(message) { }
        public ExpressionIsNullOrEmptyException(string message, Exception inner) : base(message, inner) { }
        protected ExpressionIsNullOrEmptyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}