using System;
using System.Globalization;
using Calculator.expression;
using Txt.Core;

namespace Calculator
{
    public class Program
    {
        private static readonly ConsoleColor DefaultForegroundColor = Console.ForegroundColor;

        private static void Main(string[] args)
        {
            ShowHelp();
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            var grammar = new CalculatorGrammar();
            grammar.Initialize();
            var lexer = grammar.Rule<Expression>("expression");
            var parser = new CalculatorParser();
            string expression = null;
            while (expression != "")
            {
                expression = PromptLine();
                if (string.IsNullOrEmpty(expression))
                {
                    return;
                }
                using (var stringTextSource = new StringTextSource(expression))
                using (var textScanner = new TextScanner(stringTextSource))
                {
                    var readResult = lexer.Read(textScanner);
                    if (readResult != null)
                    {
                        var textRepresentation = readResult.Text;
                        var parseResult = parser.ParseExpression(readResult);
                        Console.WriteLine("{0}={1}", textRepresentation, parseResult);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input detected");
                        Console.ForegroundColor = ConsoleColor.Red;
                        using (var reader = new TextSourceReader(stringTextSource))
                        {
                            Console.WriteLine(reader.ReadToEnd());
                        }
                        Console.ForegroundColor = DefaultForegroundColor;
                    }
                }
            }
        }

        private static string PromptLine()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter an expression: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            var expression = Console.ReadLine();
            Console.ForegroundColor = DefaultForegroundColor;
            return expression;
        }

        private static void ShowHelp()
        {
            Console.WriteLine("This sample implements a grammar and parser for simple arithmetic expressions!");
            Console.WriteLine("Examples:");
            Console.WriteLine("-6+19");
            Console.WriteLine("12+(-4)");
            Console.WriteLine("-34+(-28)");
            Console.WriteLine("266+(-265)");
            Console.WriteLine("5*3/5+(-4*1/2)");
            Console.WriteLine("(-3/4)+2");
            Console.WriteLine("(-5/6)+(-5)");
            Console.WriteLine("(3*1/5)+(2*5/8)");
        }
    }
}
