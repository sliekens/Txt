// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HorizontalTabLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
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
            if (scanner.TryMatch('\x09'))
            {
                element = new HorizontalTab(context);
                return true;
            }

            element = default(HorizontalTab);
            return false;
        }
    }
}