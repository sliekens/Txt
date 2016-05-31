using System;

using Txt.ABNF;
using Txt.Core;

namespace Sample1.term
{
    public sealed class TermLexer : Lexer<Term>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public TermLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Term> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Term>.FromResult(new Term(result.Element));
            }
            return ReadResult<Term>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}
