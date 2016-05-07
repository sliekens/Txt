using System;
using Txt.ABNF;
using Txt.Core;

namespace Txt.UTF8.UTF8_4
{
    public sealed class Utf8Octet4Lexer : Lexer<Utf8Octet4>
    {
        private readonly ILexer<Alternation> innerLexer;

        public Utf8Octet4Lexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<Utf8Octet4> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Utf8Octet4>.FromResult(new Utf8Octet4(result.Element));
            }
            return ReadResult<Utf8Octet4>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
