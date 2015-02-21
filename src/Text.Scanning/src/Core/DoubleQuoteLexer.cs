// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DoubleQuoteLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    /// <summary>TODO </summary>
    public class DoubleQuoteLexer : Lexer<DoubleQuote>
    {
        /// <inheritdoc />
        public override DoubleQuote Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            DoubleQuote element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'DQUOTE'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out DoubleQuote element)
        {
            if (scanner.EndOfInput)
            {
                element = default(DoubleQuote);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('\"'))
            {
                element = new DoubleQuote(context);
                return true;
            }

            element = default(DoubleQuote);
            return false;
        }
    }
}