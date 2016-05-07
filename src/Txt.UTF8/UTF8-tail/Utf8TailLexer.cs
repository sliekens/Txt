using System;
using Txt.ABNF;
using Txt.Core;

namespace Txt.UTF8.UTF8_tail
{
    public sealed class Utf8TailLexer : Lexer<Utf8Tail>
    {
        private readonly ILexer<Terminal> innerLexer;

        public Utf8TailLexer(ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<Utf8Tail> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Utf8Tail>.FromResult(new Utf8Tail(result.Element));
            }
            return ReadResult<Utf8Tail>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
