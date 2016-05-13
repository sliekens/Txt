using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Txt.UTF8.UTF8_octets
{
    public sealed class Utf8OctetsLexer : Lexer<Utf8Octets>
    {
        private readonly ILexer<Repetition> innerLexer;

        public Utf8OctetsLexer([NotNull] ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<Utf8Octets> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Utf8Octets>.FromResult(new Utf8Octets(result.Element));
            }
            return ReadResult<Utf8Octets>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
