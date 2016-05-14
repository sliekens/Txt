using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Txt.UTF8.UTF8_1
{
    public sealed class Utf8Octet1Lexer : Lexer<Utf8Octet1>
    {
        private readonly ILexer<Terminal> innerLexer;

        public Utf8Octet1Lexer([NotNull] ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<Utf8Octet1> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Utf8Octet1>.FromResult(new Utf8Octet1(result.Element));
            }
            return ReadResult<Utf8Octet1>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
