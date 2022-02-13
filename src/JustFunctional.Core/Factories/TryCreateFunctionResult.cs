using System.Collections.Generic;

namespace JustFunctional.Core
{
    public class TryCreateFunctionResult
    {
        private readonly List<string> _errors;
        public bool Success { get; }
        public IEnumerable<string> Errors => _errors.AsReadOnly();
        public Function? Function { get; }

        private TryCreateFunctionResult(bool success, List<string>? errors, Function? function)
        {
            Success = success;
            _errors = errors ?? new List<string>();
            Function = function;
        }

        public static TryCreateFunctionResult Successful(Function function)
        {
            return new TryCreateFunctionResult(true, null, function);
        }

        public static TryCreateFunctionResult Failure(List<string> errors)
        {
            return new TryCreateFunctionResult(false, errors, null);
        }
    }
}