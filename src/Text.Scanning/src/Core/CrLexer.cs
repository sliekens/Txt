// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CrLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   The cr lexer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    /// <summary>The cr lexer.</summary>
    public class CrLexer : Lexer<CrToken>
    {
        /// <inheritdoc />
        public override CrToken Read(ITextScanner scanner)
        {
            CrToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'CR'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out CrToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(CrToken);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('\u000D'))
            {
                token = new CrToken(context);
                return true;
            }

            token = default(CrToken);
            return false;
        }
    }
}