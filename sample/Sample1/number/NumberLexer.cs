using System;
using Txt.ABNF;
using Txt.Core;

namespace Sample1.number
{
    public sealed class NumberLexer : Lexer<Number>
    {
        private readonly ILexer<Repetition> innerLexer;

        public NumberLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        protected override ReadResult<Number> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return new ReadResult<Number>(new Number(result.Element));
            }
            return new ReadResult<Number>(SyntaxError.FromReadResult(result, context));
        }
    }
}
