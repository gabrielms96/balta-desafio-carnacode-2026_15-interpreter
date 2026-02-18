using DesignPatternChallenge.Models;

namespace DesignPatternChallenge.Interpreter.Expressions.NonTerminal
{
    public class NotExpression : Expression
    {
        private readonly Expression _expression;

        public NotExpression(Expression expression)
        {
            _expression = expression;
        }

        public override bool Interpret(ShoppingCart context)
        {
            return !_expression.Interpret(context);
        }
    }
}