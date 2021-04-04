using System;
namespace JustFunctional.Core
{
    [Serializable]
    public class SyntaxErrorInExpressionException : JustFunctionalBaseException
    {
        public SyntaxErrorInExpressionException() { }
        public SyntaxErrorInExpressionException(string message) : base(message) { }
        public SyntaxErrorInExpressionException(string message, Exception inner) : base(message, inner) { }

        protected SyntaxErrorInExpressionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}