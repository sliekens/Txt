using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Txt.UTF8.UTF8_2
{
    public sealed class Utf8Octet2Lexer : Lexer<Utf8Octet2>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public Utf8Octet2Lexer([NotNull] ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<Utf8Octet2> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Utf8Octet2>.FromResult(new Utf8Octet2(result.Element));
            }
            return ReadResult<Utf8Octet2>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
