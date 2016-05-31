using Sample1.expression;
using Sample1.factor;
using Sample1.number;
using Sample1.term;
using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace Sample1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var expression = @"1+(1-1)";
            var lexer = GetExpressionLexer();
            using (var stringTextSource = new StringTextSource(expression))
            using (var textScanner = new TextScanner(stringTextSource))
            {
                var readResult = lexer.Read(textScanner);
            }
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
                repetitionLexerFactory,
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
