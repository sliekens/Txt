// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlphaLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   The alpha lexer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    /// <summary>The alpha lexer.</summary>
    public class AlphaLexer : Lexer<AlphaToken>
    {
        /// <inheritdoc />
        public override AlphaToken Read(ITextScanner scanner)
        {
            AlphaToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'ALPHA'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out AlphaToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(AlphaToken);
                return false;
            }

            var context = scanner.GetContext();

            // A - Z
            for (var c = '\u0041'; c <= '\u005A'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    token = new AlphaToken(c, context);
                    return true;
                }
            }

            // a - z
            for (var c = '\u0061'; c <= '\u007A'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    token = new AlphaToken(c, context);
                    return true;
                }
            }

            token = default(AlphaToken);
            return false;
        }
    }
}