// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlphaLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   TODO
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    /// <summary>TODO </summary>
    public class AlphaLexer : Lexer<Alpha>
    {
        /// <summary>Initializes a new instance of the <see cref="AlphaLexer"/> class.</summary>
        public AlphaLexer()
            : base("ALPHA")
        {
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out Alpha element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Alpha);
                return false;
            }

            var context = scanner.GetContext();

            // A - Z
            for (var c = '\x41'; c <= '\x5A'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new Alpha(c, context);
                    return true;
                }
            }

            // a - z
            for (var c = '\x61'; c <= '\x7A'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new Alpha(c, context);
                    return true;
                }
            }

            element = default(Alpha);
            return false;
        }
    }
}