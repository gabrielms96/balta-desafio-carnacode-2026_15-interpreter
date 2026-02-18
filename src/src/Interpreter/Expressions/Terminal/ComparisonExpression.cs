using DesignPatternChallenge.Models;

namespace DesignPatternChallenge.Interpreter.Expressions.Terminal
{
    public class ComparisonExpression : Expression
    {
        private readonly VariableExpression _variable;
        private readonly string _operator;
        private readonly object _value;

        public ComparisonExpression(VariableExpression variable, string op, object value)
        {
            _variable = variable;
            _operator = op;
            _value = value;
        }

        public override bool Interpret(ShoppingCart context)
        {
            return _operator switch
            {
                ">" => _variable.GetValue(context) > Convert.ToDecimal(_value),
                ">=" => _variable.GetValue(context) >= Convert.ToDecimal(_value),
                "<" => _variable.GetValue(context) < Convert.ToDecimal(_value),
                "<=" => _variable.GetValue(context) <= Convert.ToDecimal(_value),
                "=" => _variable.GetStringValue(context) == _value.ToString(),
                "==" => _variable.GetBoolValue(context) == Convert.ToBoolean(_value),
                _ => false
            };
        }
    }
}