using Txt;
using Txt.ABNF;

namespace Sample1
{
    using System;
    

    public sealed class SignLexer : Lexer<Sign>
    {
        private readonly ILexer<Alternative> innerLexer;

        public SignLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<Sign> Read(ITextScanner scanner)
        {
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                var sign = new Sign(result.Element);
                return ReadResult<Sign>.FromResult(sign);
            }
            var syntaxError = SyntaxError.FromReadResult(result, scanner.GetContext());
            return ReadResult<Sign>.FromSyntaxError(syntaxError);
        }
    }
}
