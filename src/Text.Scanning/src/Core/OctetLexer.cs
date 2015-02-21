// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OctetLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    /// <summary>TODO </summary>
    public class OctetLexer : Lexer<Octet>
    {
        /// <inheritdoc />
        public override Octet Read(ITextScanner scanner)
        {
            Octet element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'OCTET'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out Octet element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Octet);
                return false;
            }

            var context = scanner.GetContext();
            for (var c = '\u0000'; c <= '\u00FF'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new Octet(c, context);
                    return true;
                }
            }

            element = default(Octet);
            return false;
        }
    }
}