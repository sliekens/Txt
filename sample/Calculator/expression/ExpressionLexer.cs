using System;
using System.Collections.Generic;
using Txt.ABNF;
using Txt.Core;

namespace Calculator.expression
{
    public sealed class ExpressionLexer : RuleLexer<Expression>, IInitializable
    {
        public ExpressionLexer(Grammar grammar)
            : base(grammar)
        {
        }

        public ILexer<Element> InnerLexer { get; private set; }

        public void Initialize()
        {
            var term = Grammar.Rule("term");
            InnerLexer = Concatenation.Create(
                term,
                Repetition.Create(
                    Concatenation.Create(
                        Alternation.Create(
                            Terminal.Create("+", StringComparer.Ordinal),
                            Terminal.Create("-", StringComparer.Ordinal)),
                        term),
                    0,
                    int.MaxValue));
        }

        protected override IEnumerable<Expression> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var concatenation in InnerLexer.Read(scanner, context))
            {
                yield return new Expression(concatenation);
            }
        }
    }
}
