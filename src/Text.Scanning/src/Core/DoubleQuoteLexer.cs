// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DoubleQuoteLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   TODO
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    /// <summary>TODO </summary>
    public class DoubleQuoteLexer : Lexer<DoubleQuote>
    {
        /// <summary>Initializes a new instance of the <see cref="DoubleQuoteLexer"/> class.</summary>
        public DoubleQuoteLexer()
            : base("DQUOTE")
        {
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