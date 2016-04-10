using Txt;

namespace Sample1
{
    using System;
    using Text;
    using Text.ABNF;

    public sealed class IntegerLexer : Lexer<Integer>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public IntegerLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<Integer> Read(ITextScanner scanner)
        {
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                var integer = new Integer(result.Element);
                return ReadResult<Integer>.FromResult(integer);
            }
            var syntaxError = SyntaxError.FromReadResult(result, scanner.GetContext());
            return ReadResult<Integer>.FromSyntaxError(syntaxError);
        }
    }
}
