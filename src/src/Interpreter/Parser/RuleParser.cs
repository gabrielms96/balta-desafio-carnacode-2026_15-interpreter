using DesignPatternChallenge.Interpreter.Expressions;
using DesignPatternChallenge.Interpreter.Expressions.NonTerminal;
using DesignPatternChallenge.Interpreter.Expressions.Terminal;
using System.Text.RegularExpressions;

namespace DesignPatternChallenge.Interpreter.Parser
{
    public class RuleParser
    {
        private readonly List<Token> _tokens;
        private int _currentToken;

        public RuleParser(string input)
        {
            _tokens = Tokenize(input);
            _currentToken = 0;
        }

        private List<Token> Tokenize(string input)
        {
            var tokens = new List<Token>();
            var pattern = @"(\(|\)|E|OU|NÃO|>=|<=|>|<|==|=|\w+|[0-9.]+)";
            var matches = Regex.Matches(input, pattern, RegexOptions.IgnoreCase);

            foreach (Match match in matches)
            {
                var value = match.Value;
                tokens.Add(new Token(ClassifyToken(value), value));
            }

            return tokens;
        }

        private string ClassifyToken(string value)
        {
            return value.ToUpper() switch
            {
                "E" => "AND",
                "OU" => "OR",
                "NÃO" => "NOT",
                "(" => "LPAREN",
                ")" => "RPAREN",
                ">=" => "GTE",
                "<=" => "LTE",
                ">" => "GT",
                "<" => "LT",
                "==" => "EQ",
                "=" => "ASSIGN",
                _ => decimal.TryParse(value, out _) ? "NUMBER" : "IDENTIFIER"
            };
        }

        private Token Current => _currentToken < _tokens.Count ? _tokens[_currentToken] : new Token("EOF", "");

        private void Advance() => _currentToken++;

        public Expression Parse()
        {
            return ParseOrExpression();
        }

        private Expression ParseOrExpression()
        {
            var left = ParseAndExpression();

            while (Current.Type == "OR")
            {
                Advance();
                var right = ParseAndExpression();
                left = new OrExpression(left, right);
            }

            return left;
        }

        private Expression ParseAndExpression()
        {
            var left = ParseNotExpression();

            while (Current.Type == "AND")
            {
                Advance();
                var right = ParseNotExpression();
                left = new AndExpression(left, right);
            }

            return left;
        }

        private Expression ParseNotExpression()
        {
            if (Current.Type == "NOT")
            {
                Advance();
                return new NotExpression(ParsePrimaryExpression());
            }

            return ParsePrimaryExpression();
        }

        private Expression ParsePrimaryExpression()
        {
            if (Current.Type == "LPAREN")
            {
                Advance();
                var expr = ParseOrExpression();
                if (Current.Type != "RPAREN")
                    throw new InvalidOperationException("Parêntese de fechamento esperado");
                Advance();
                return expr;
            }

            if (Current.Type == "IDENTIFIER")
            {
                var variable = Current.Value;
                Advance();

                if (IsComparisonOperator(Current.Type))
                {
                    var op = Current.Value;
                    Advance();

                    if (Current.Type == "NUMBER")
                    {
                        var numValue = decimal.Parse(Current.Value);
                        Advance();
                        return new ComparisonExpression(new VariableExpression(variable), op, numValue);
                    }
                    else if (Current.Type == "IDENTIFIER")
                    {
                        var strValue = Current.Value;
                        Advance();
                        return new ComparisonExpression(new VariableExpression(variable), op, strValue);
                    }
                }
                else if (Current.Type == "ASSIGN" || Current.Type == "EQ")
                {
                    var op = Current.Type == "ASSIGN" ? "=" : "==";
                    Advance();

                    if (Current.Type == "IDENTIFIER")
                    {
                        var value = Current.Value;
                        Advance();
                        return new ComparisonExpression(new VariableExpression(variable), op, value);
                    }
                    else if (Current.Type == "NUMBER")
                    {
                        var value = decimal.Parse(Current.Value);
                        Advance();
                        return new ComparisonExpression(new VariableExpression(variable), op, value);
                    }
                }

                throw new InvalidOperationException($"Operador esperado após '{variable}'");
            }

            throw new InvalidOperationException($"Token inesperado: {Current.Value}");
        }

        private bool IsComparisonOperator(string type)
        {
            return type is "GT" or "GTE" or "LT" or "LTE";
        }
    }
}