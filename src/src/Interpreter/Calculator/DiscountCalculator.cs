using DesignPatternChallenge.Interpreter.Parser;
using DesignPatternChallenge.Models;

namespace DesignPatternChallenge.Interpreter.Calculator
{
    public class DiscountCalculator
    {
        public decimal CalculateDiscount(ShoppingCart cart, string ruleText)
        {
            Console.WriteLine($"Avaliando regra: {ruleText}");

            try
            {
                var parts = ruleText.Split("ENTAO", StringSplitOptions.None);
                if (parts.Length != 2)
                {
                    Console.WriteLine("✗ Formato inválido. Use: <condição> ENTAO <desconto>");
                    return 0;
                }

                var conditionText = parts[0].Trim();
                var discountText = parts[1].Trim();

                if (!decimal.TryParse(discountText, out decimal discount))
                {
                    Console.WriteLine("✗ Desconto inválido");
                    return 0;
                }

                var parser = new RuleParser(conditionText);
                var expression = parser.Parse();

                if (expression.Interpret(cart))
                {
                    Console.WriteLine($"✓ Regra aplicada: {discount}% desconto");
                    return discount;
                }

                Console.WriteLine("✗ Regra não aplicável");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Erro ao processar regra: {ex.Message}");
                return 0;
            }
        }
    }
}