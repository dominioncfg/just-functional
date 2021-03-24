namespace JustFunctional.Core
{
    internal interface ITokenizer
    {
        IToken GetNextToken();
        void Reset();
    }
}