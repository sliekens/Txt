using System;
using System.Globalization;
using Calculator.expression;
using Calculator.factor;
using Calculator.number;
using Calculator.term;
using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace Calculator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var lexer = GetExpressionLexer();
            var parser = GetExpressionParser();
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            var foregroundColor = Console.ForegroundColor;
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
            string expression = null;
            while (expression != "")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter an expression: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                expression = Console.ReadLine();
                Console.ForegroundColor = foregroundColor;
                if (expression == "")
                {
                    return;
                }
                using (var stringTextSource = new StringTextSource(expression))
                using (var textScanner = new TextScanner(stringTextSource))
                {
                    var readResult = lexer.Read(textScanner);
                    if (readResult.IsSuccess)
                    {
                        Console.WriteLine(
                            "{0}={1}",
                            readResult.Element.Text,
                            parser.Parse(readResult.Element));
                    }
                    else
                    {
                        Console.WriteLine("Invalid input detected");
                        Console.ForegroundColor = ConsoleColor.Red;
                        using (var reader = new TextSourceReader(stringTextSource))
                        {
                            Console.WriteLine(reader.ReadToEnd());
                        }
                        Console.ForegroundColor = foregroundColor;
                    }
                }
            }
        }

        private static ExpressionParser GetExpressionParser()
        {
            var expressionParser = new ProxyParser<Expression, double>();
            var parser = new ExpressionParser(new TermParser(new FactorParser(new NumberParser(), expressionParser)));
            expressionParser.Initialize(parser);
            return parser;
        }

        private static ILexer<Expression> GetExpressionLexer()
        {
            var concatenationLexerFactory = new ConcatenationLexerFactory();
            var repetitionLexerFactory = new RepetitionLexerFactory();
            var alternationLexerFactory = new AlternationLexerFactory();
            var terminalLexerFactory = new TerminalLexerFactory();
            var optionLexerFactory = new OptionLexerFactory();
            var valueRangeLexerFactory = new ValueRangeLexerFactory();
            var digitLexerFactory = new DigitLexerFactory(valueRangeLexerFactory);
            var digitLexer = digitLexerFactory.Create();
            var numberLexerFactory = new NumberLexerFactory(
                alternationLexerFactory,
                concatenationLexerFactory,
                repetitionLexerFactory,
                optionLexerFactory,
                terminalLexerFactory,
                digitLexer);
            var numberLexer = numberLexerFactory.Create();
            var expressionLexerProxy = new ProxyLexer<Expression>();
            var factorLexerFactory = new FactorLexerFactory(
                concatenationLexerFactory,
                optionLexerFactory,
                terminalLexerFactory,
                alternationLexerFactory,
                numberLexer,
                expressionLexerProxy);
            var factorLexer = factorLexerFactory.Create();
            var termLexerFactory = new TermLexerFactory(
                concatenationLexerFactory,
                repetitionLexerFactory,
                alternationLexerFactory,
                terminalLexerFactory,
                factorLexer);
            var termLexer = termLexerFactory.Create();
            var expressionLexerFactory = new ExpressionLexerFactory(
                concatenationLexerFactory,
                repetitionLexerFactory,
                alternationLexerFactory,
                terminalLexerFactory,
                termLexer);
            var expressionLexer = expressionLexerFactory.Create();
            expressionLexerProxy.Initialize(expressionLexer);
            return expressionLexer;
        }
    }
}
