using System;
namespace JustFunctional.Core
{
    [Serializable]
    public class JustFunctionalBaseException : Exception
    {
        public JustFunctionalBaseException() { }
        public JustFunctionalBaseException(string message) : base(message) { }
        public JustFunctionalBaseException(string message, Exception inner) : base(message, inner) { }
        protected JustFunctionalBaseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}