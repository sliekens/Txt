using System;
using Txt.ABNF;
using Txt.Core;

namespace Sample1.factor
{
    public sealed class FactorLexer : Lexer<Factor>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public FactorLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        protected override ReadResult<Factor> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Factor>.FromResult(new Factor(result.Element));
            }
            return ReadResult<Factor>.FromSyntaxError(SyntaxError.FromReadResult(result, context));
        }
    }
}
