using DesignPatternChallenge.Models;

namespace DesignPatternChallenge.Interpreter.Expressions.NonTerminal
{
    public class OrExpression : Expression
    {
        private readonly Expression _left;
        private readonly Expression _right;

        public OrExpression(Expression left, Expression right)
        {
            _left = left;
            _right = right;
        }

        public override bool Interpret(ShoppingCart context)
        {
            return _left.Interpret(context) || _right.Interpret(context);
        }
    }
}