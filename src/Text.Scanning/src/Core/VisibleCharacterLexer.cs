// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VisibleCharacterLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    /// <summary>TODO </summary>
    public class VisibleCharacterLexer : Lexer<VisibleCharacter>
    {
        /// <inheritdoc />
        public override VisibleCharacter Read(ITextScanner scanner)
        {
            VisibleCharacter element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'VCHAR'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out VisibleCharacter element)
        {
            if (scanner.EndOfInput)
            {
                element = default(VisibleCharacter);
                return false;
            }

            var context = scanner.GetContext();
            for (var c = '\u0021'; c < '\u007E'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new VisibleCharacter(c, context);
                    return true;
                }
            }

            element = default(VisibleCharacter);
            return false;
        }
    }
}