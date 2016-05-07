using System;
using Txt.ABNF;
using Txt.Core;

namespace Txt.UTF8.UTF8_3
{
    public sealed class Utf8Octet3Lexer : Lexer<Utf8Octet3>
    {
        private readonly ILexer<Alternation> innerLexer;

        public Utf8Octet3Lexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<Utf8Octet3> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Utf8Octet3>.FromResult(new Utf8Octet3(result.Element));
            }
            return ReadResult<Utf8Octet3>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
