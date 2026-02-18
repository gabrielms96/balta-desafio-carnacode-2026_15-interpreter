using DesignPatternChallenge.Models;

namespace DesignPatternChallenge.Interpreter.Expressions.Terminal
{
    public class NumberExpression : Expression
    {
        private readonly decimal _value;

        public NumberExpression(decimal value)
        {
            _value = value;
        }

        public override bool Interpret(ShoppingCart context) => _value != 0;

        public decimal GetValue() => _value;
    }
}