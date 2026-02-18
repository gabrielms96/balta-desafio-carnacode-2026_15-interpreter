using DesignPatternChallenge.Models;

namespace DesignPatternChallenge.Interpreter.Expressions.NonTerminal
{
    public class AndExpression : Expression
    {
        private readonly Expression _left;
        private readonly Expression _right;

        public AndExpression(Expression left, Expression right)
        {
            _left = left;
            _right = right;
        }

        public override bool Interpret(ShoppingCart context)
        {
            return _left.Interpret(context) && _right.Interpret(context);
        }
    }
}