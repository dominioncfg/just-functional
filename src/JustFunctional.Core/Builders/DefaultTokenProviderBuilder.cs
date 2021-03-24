namespace JustFunctional.Core
{
    public class DefaultTokenProviderBuilder : ITokenProviderBuilder
    {
        public ITokensProvider Build() => new DefaultTokensProvider();
    }
}