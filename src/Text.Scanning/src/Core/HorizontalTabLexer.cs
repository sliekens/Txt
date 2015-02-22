// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HorizontalTabLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   TODO
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    /// <summary>TODO </summary>
    public class HorizontalTabLexer : Lexer<HorizontalTab>
    {
        /// <summary>Initializes a new instance of the <see cref="HorizontalTabLexer"/> class.</summary>
        public HorizontalTabLexer()
            : base("HTAB")
        {
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out HorizontalTab element)
        {
            if (scanner.EndOfInput)
            {
                element = default(HorizontalTab);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('\t'))
            {
                element = new HorizontalTab(context);
                return true;
            }

            element = default(HorizontalTab);
            return false;
        }
    }
}