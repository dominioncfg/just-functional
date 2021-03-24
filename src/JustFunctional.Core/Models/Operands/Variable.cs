namespace JustFunctional.Core
{
    public class Variable : Operand
    {
        public Variable(string identifier) : base(0, identifier) { }
        public override string GetValue() => RawToken.ToString();
    }
}