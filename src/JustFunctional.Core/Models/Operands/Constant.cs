namespace JustFunctional.Core
{
    public class Constant : Operand
    {
        public Constant(string identifier, decimal value) : base(value,identifier) { }
    }
}