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

        public override ReadResult<Number> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Number>.FromResult(new Number(result.Element));
            }
            return ReadResult<Number>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
