namespace JustFunctional.Core
{
    public class Operand : IToken
    {
        public string RawToken { get; }
        public decimal Value { get; }
        public virtual string GetValue() => Value.ToString();

        public Operand(decimal value) : this(value, value.ToString()) { }

        public Operand(decimal value, string token)
        {
            RawToken = token;
            Value = value;
        }
    }
}