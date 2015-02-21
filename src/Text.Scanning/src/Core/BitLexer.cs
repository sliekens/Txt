// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   The bit lexer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    /// <summary>The bit lexer.</summary>
    public class BitLexer : Lexer<BitToken>
    {
        /// <inheritdoc />
        public override BitToken Read(ITextScanner scanner)
        {
            BitToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'BIT'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out BitToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(BitToken);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('\u0030'))
            {
                token = new BitToken('\u0030', context);
                return true;
            }

            if (scanner.TryMatch('\u0031'))
            {
                token = new BitToken('\u0031', context);
                return true;
            }

            token = default(BitToken);
            return false;
        }
    }
}