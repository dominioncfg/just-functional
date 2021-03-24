using System;
namespace JustFunctional.Core
{
    [Serializable]
    public class VariableUndefinedException : JustFunctionalBaseException
    {
        public VariableUndefinedException() { }
        public VariableUndefinedException(string message) : base(message) { }
        public VariableUndefinedException(string message, Exception inner) : base(message, inner) { }
        protected VariableUndefinedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}