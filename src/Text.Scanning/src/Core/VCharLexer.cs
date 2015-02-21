// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VCharLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   The v char lexer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    /// <summary>The v char lexer.</summary>
    public class VCharLexer : Lexer<VCharToken>
    {
        /// <inheritdoc />
        public override VCharToken Read(ITextScanner scanner)
        {
            VCharToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'VCHAR'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out VCharToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(VCharToken);
                return false;
            }

            var context = scanner.GetContext();
            for (var c = '\u0021'; c < '\u007E'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    token = new VCharToken(c, context);
                    return true;
                }
            }

            token = default(VCharToken);
            return false;
        }
    }
}