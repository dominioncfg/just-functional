using System.Collections.Generic;
namespace JustFunctional.Core
{
    internal interface ITokenReader<T> : IEnumerator<T>
    {
        public int CurrentIndex { get; }
        bool MovePrevious();
    }
}