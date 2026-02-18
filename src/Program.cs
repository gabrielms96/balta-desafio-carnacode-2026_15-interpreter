using DesignPatternChallenge.Interpreter.Calculator;
using DesignPatternChallenge.Models;

namespace DesignPatternChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Regras de Desconto com Interpreter Pattern ===\n");

            var calculator = new DiscountCalculator();

            var cart1 = new ShoppingCart
            {
                TotalValue = 1500.00m,
                ItemQuantity = 15,
                CustomerCategory = "Regular",
                IsFirstPurchase = false
            };

            var cart2 = new ShoppingCart
            {
                TotalValue = 500.00m,
                ItemQuantity = 5,
                CustomerCategory = "VIP",
                IsFirstPurchase = false
            };

            var cart3 = new ShoppingCart
            {
                TotalValue = 200.00m,
                ItemQuantity = 2,
                CustomerCategory = "Regular",
                IsFirstPurchase = true
            };

            var rules = new List<string>
            {
                "quantidade > 10 E valor > 1000 ENTAO 15",
                "categoria = VIP ENTAO 20",
                "primeiraCompra == true ENTAO 10",
                "(quantidade > 10 OU valor > 500) E categoria = VIP ENTAO 25",
                "NÃO primeiraCompra E quantidade >= 5 ENTAO 12"
            };

            Console.WriteLine("=== Carrinho 1 (quantidade=15, valor=1500, categoria=Regular) ===");
            foreach (var rule in rules)
            {
                calculator.CalculateDiscount(cart1, rule);
            }

            Console.WriteLine("\n=== Carrinho 2 (quantidade=5, valor=500, categoria=VIP) ===");
            foreach (var rule in rules)
            {
                calculator.CalculateDiscount(cart2, rule);
            }

            Console.WriteLine("\n=== Carrinho 3 (quantidade=2, valor=200, categoria=Regular, primeira=true) ===");
            foreach (var rule in rules)
            {
                calculator.CalculateDiscount(cart3, rule);
            }
        }
    }
}