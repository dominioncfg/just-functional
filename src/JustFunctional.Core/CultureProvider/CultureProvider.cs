using System.Globalization;
namespace JustFunctional.Core
{
    public class CultureProvider : ICultureProvider
    {
        private readonly CultureInfo _culture;
        public CultureProvider(CultureInfo? culture = null)
        {
            _culture = culture ?? CultureInfo.CurrentCulture;
        }

        public CultureInfo GetCulture() => _culture;
    }
}