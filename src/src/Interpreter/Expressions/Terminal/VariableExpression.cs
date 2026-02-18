using DesignPatternChallenge.Models;

namespace DesignPatternChallenge.Interpreter.Expressions.Terminal
{
    public class VariableExpression : Expression
    {
        private readonly string _variable;

        public VariableExpression(string variable)
        {
            _variable = variable;
        }

        public decimal GetValue(ShoppingCart context) => _variable switch
        {
            "quantidade" => context.ItemQuantity,
            "valor" => context.TotalValue,
            _ => 0
        };

        public string GetStringValue(ShoppingCart context) => _variable switch
        {
            "categoria" => context.CustomerCategory,
            _ => ""
        };

        public bool GetBoolValue(ShoppingCart context) => _variable switch
        {
            "primeiraCompra" => context.IsFirstPurchase,
            _ => false
        };

        public override bool Interpret(ShoppingCart context) => true;
    }
}