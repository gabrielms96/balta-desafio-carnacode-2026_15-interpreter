using DesignPatternChallenge.Models;

namespace DesignPatternChallenge.Interpreter.Expressions
{
    public abstract class Expression
    {
        public abstract bool Interpret(ShoppingCart context);
    }
}